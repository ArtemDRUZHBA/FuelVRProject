using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GuidedProcedure : MonoBehaviour
{
    [Header("eSpeak settings")]
    [SerializeField] private string apiKey = "72fd1cc60e5a1fbdf17bec63912c4da9c7b89a25c45832cd7a584ad5b167e0d6";
    [SerializeField] private string voiceId = "qJBO8ZmKp4te7NTtYgzz";
    [SerializeField] private float preSpeakDelay = 0.2f;

    [Header("Steps source")]
    [TextArea(6, 12)]
    [SerializeField] private string stepsText;

    private readonly List<string> steps = new List<string>();
    private int currentIndex = -1;
    private bool awaitingUser = false;
    private bool speaking = false;

    public event Action<int, string> OnStepShown;
    public event Action OnFinished;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        foreach (var raw in stepsText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None))
        {
            var line = raw.Trim();
            if (!string.IsNullOrEmpty(line))
                steps.Add(line);
        }
        StartCoroutine(BeginAfterDelay());
    }

    private IEnumerator BeginAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        StartProcedure();
    }

    public void StartProcedure()
    {
        if (steps.Count == 0)
        {
            UnityEngine.Debug.LogWarning("GuidedProcedure: нет шагов для озвучивания.");
            return;
        }
        currentIndex = -1;
        NextStep();
    }

    public async void NextStep()
    {
        if (speaking || awaitingUser) return;

        currentIndex++;
        if (currentIndex >= steps.Count)
        {
            OnFinished?.Invoke();
            UnityEngine.Debug.Log("GuidedProcedure: процедура завершена..");
            return;
        }

        var text = steps[currentIndex];
        OnStepShown?.Invoke(currentIndex, text);

        speaking = true;
        await Task.Delay(TimeSpan.FromSeconds(preSpeakDelay));
        await SpeakAsync(text);
        speaking = false;

        awaitingUser = true;
        UnityEngine.Debug.Log($"GuidedProcedure: ждём подтверждения выполнения шага {currentIndex + 1}.");
    }

    public void ConfirmStepDone()
    {
        if (!awaitingUser) return;
        awaitingUser = false;
        NextStep();
    }

    public void StopProcedure()
    {
        awaitingUser = false;
        speaking = false;
        currentIndex = -1;
        UnityEngine.Debug.Log("GuidedProcedure: процедура остановлена.");
    }

    private async Task SpeakAsync(string text)
    {
        string url = $"https://api.elevenlabs.io/v1/text-to-speech/{voiceId}";

        string json = "{\"text\":\"" + text + "\",\"model_id\":\"eleven_multilingual_v2\",\"voice_settings\":{\"stability\":0.5,\"similarity_boost\":0.5},\"output_format\":\"wav\"}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("xi-api-key", apiKey);

            var operation = request.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                byte[] audioData = request.downloadHandler.data;
                string tempPath = System.IO.Path.Combine(Application.persistentDataPath, "tts.mp3");
                System.IO.File.WriteAllBytes(tempPath, audioData);

                UnityEngine.Debug.Log("MP3 сохранён: " + tempPath);
            }
            else
            {
                UnityEngine.Debug.LogError("ElevenLabs TTS error: " + request.error + " | " + request.downloadHandler.text);
            }
        }
    }


}
