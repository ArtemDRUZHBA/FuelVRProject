using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class WhisperTest : MonoBehaviour
{
    [DllImport("whisper")]
    private static extern IntPtr whisper_init(string modelPath);

    void Start()
    {
        string modelPath = Path.Combine(Application.streamingAssetsPath, "Models/ggml-base-q5_1.bin");
        Debug.Log(File.Exists(modelPath) ? "Модель найдена" : "Модель НЕ найдена");

        var ctx = whisper_init(modelPath);
        Debug.Log(ctx != IntPtr.Zero ? "Whisper инициализирован" : "Ошибка инициализации Whisper");
    }
}
