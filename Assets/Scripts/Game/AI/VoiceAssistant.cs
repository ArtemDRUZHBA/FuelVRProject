using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Vosk;
using Newtonsoft.Json.Linq;

public class VoiceAssistant : MonoBehaviour
{
    public string voskModelPath = "vosk-model-ru-0.22"; // Папка в StreamingAssets
    public string openAiApiKey = "sk-9ed17c44de6844aa9505403322607801"; // Вставь свой ключ
    private VoskRecognizer recognizer;
    private Model model;
    private AudioClip micClip;
    private const int sampleRate = 16000;
    private int lastSamplePos = 0;

    private bool _isListening = false;
    private bool _isProcessing = false;

    private bool _playerInside = false;

    public event Action<string> OnTextRecognized;


    void Start()
    {
        string modelPath = Path.Combine(Application.streamingAssetsPath, voskModelPath);
        if (!Directory.Exists(modelPath))
        {
            UnityEngine.Debug.LogError("Модель не найдена: " + modelPath);
            return;
        }

        Vosk.Vosk.SetLogLevel(0);
        model = new Model(modelPath);
        recognizer = new VoskRecognizer(model, sampleRate);

        UnityEngine.Debug.Log("Модель загружена");
    }

    void Update()
    {
        if (!_playerInside)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            StartListening();

        if (Input.GetKeyUp(KeyCode.Space))
            StopListening();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInside = false;
            StopListening();
        }
    }


    void StartListening()
    {
        if (_isProcessing) return;
        if (Microphone.devices.Length == 0)
        {
            UnityEngine.Debug.LogError("Микрофон не найден!");
            return;
        }

        string micName = Microphone.devices[0];
        UnityEngine.Debug.Log("Используем устройство: " + micName);

        micClip = Microphone.Start(micName, true, 10, sampleRate);
        if (micClip == null)
        {
            UnityEngine.Debug.LogError("Не удалось запустить запись!");
            return;
        }

        lastSamplePos = 0;
        _isListening = true;
        StartCoroutine(RecognizeLoop());
        UnityEngine.Debug.Log("Начало прослушивания");
    }

    void StopListening()
    {
        _isListening = false;
        Microphone.End(null);

        // Получаем финальный результат от Vosk
        string final = recognizer.FinalResult();
        HandleRecognizedText(final);

        // Сбросим распознаватель, чтобы не тянуть старые partial
        recognizer?.Reset();

        UnityEngine.Debug.Log("Окончание прослушивания");
    }


    IEnumerator RecognizeLoop()
    {
        int frameSize = 1024;
        float[] samples = new float[frameSize];
        short[] int16Samples = new short[frameSize];

        while (_isListening)
        {
            int currentPos = Microphone.GetPosition(null);
            if (currentPos < lastSamplePos) lastSamplePos = 0;

            int length = currentPos - lastSamplePos;
            if (length >= frameSize && lastSamplePos + frameSize <= micClip.samples)
            {
                micClip.GetData(samples, lastSamplePos);

                for (int i = 0; i < frameSize; i++)
                {
                    int16Samples[i] = (short)Mathf.Clamp(samples[i] * 32767f, short.MinValue, short.MaxValue);
                }

                byte[] pcm = new byte[frameSize * 2];
                Buffer.BlockCopy(int16Samples, 0, pcm, 0, pcm.Length);

                if (recognizer.AcceptWaveform(pcm, pcm.Length))
                {
                    string result = recognizer.Result();
                    UnityEngine.Debug.Log("JSON от Vosk: " + result);
                }
                else
                {
                    UnityEngine.Debug.Log("Промежуточно: " + recognizer.PartialResult());
                }

                lastSamplePos += frameSize;
            }

            yield return null;
        }
    }
    private void HandleRecognizedText(string json)
    {
        string text = ExtractText(json);
        if (string.IsNullOrEmpty(text) || _isProcessing) return;

        UnityEngine.Debug.Log("Распознано: " + text);

        OnTextRecognized?.Invoke(text);

        _isProcessing = true;
        StartCoroutine(SendToGPT(text));
    }


    private string ExtractText(string json)
    {
        UnityEngine.Debug.Log("ExtractText start work");
        try
        {
            var obj = JObject.Parse(json);
            return (string)obj["text"] ?? "";
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("Ошибка парсинга JSON: " + e.Message);
            return "";
        }
    }

    IEnumerator SendToGPT(string prompt)
    {
        UnityEngine.Debug.Log("SendToGPT start work");
        if (string.IsNullOrWhiteSpace(prompt))
        {
            UnityEngine.Debug.Log("Пустой запрос. GPT не вызывается.");
            _isProcessing = false;
            yield break;
        }

        string url = "https://api.deepseek.com/chat/completions";
        string json = "{\"model\":\"deepseek-chat\",\"messages\":[{\"role\":\"user\",\"content\":\"" + prompt + "\"}]}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + openAiApiKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            string reply = ExtractReply(response);
            UnityEngine.Debug.Log("GPT ответ: " + reply);
            Speak(reply);
        }
        else
        {
            UnityEngine.Debug.LogError("GPT ошибка: " + request.error + "\n" + request.downloadHandler.text);
        }
        _isProcessing = false;
    }

    string ExtractReply(string json)
    {
        int idx = json.IndexOf("\"content\"");
        if (idx < 0) return "";
        int start = json.IndexOf("\"", idx + 10) + 1;
        int end = json.IndexOf("\"", start);
        if (end < 0) return "";
        string content = json.Substring(start, end - start);
        return content.Replace("\\n", "\n").Replace("\\\"", "\"");
    }

    void Speak(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return;

        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = @"C:\Program Files\eSpeak NG\espeak-ng.exe"; // путь к eSpeak NG
        psi.Arguments = $"\"{text}\" -v ru";
        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardError = true;
        psi.RedirectStandardOutput = true;

        var proc = Process.Start(psi);
        proc.WaitForExit();
    }

    void OnDestroy()
    {
        Microphone.End(null);
        recognizer?.Dispose();
        model?.Dispose();
    }
}