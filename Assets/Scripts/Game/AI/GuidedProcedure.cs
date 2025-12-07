using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class GuidedProcedure : MonoBehaviour
{
    [Header("eSpeak settings")]
    [SerializeField] private string voice = "ru";
    [SerializeField] private int speed = 140;
    [SerializeField] private int pitch = 50;
    [SerializeField] private float preSpeakDelay = 0.2f;

    [Header("Steps source")]
    [TextArea(6, 12)]
    [SerializeField] private string stepsText;

    private readonly List<string> steps = new List<string>();
    private int currentIndex = -1;
    private bool awaitingUser = false;
    private bool speaking = false;

    // События для UI/логики (подсветка текущего шага, окончание процедуры)
    public event Action<int, string> OnStepShown;
    public event Action OnFinished;

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
            UnityEngine.Debug.Log("GuidedProcedure: процедура завершена.");
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

    private Task SpeakAsync(string text)
    {
        return Task.Run(() =>
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "espeak",
                    Arguments = $"\"{text}\" -v {voice} -s {speed} -p {pitch}",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (var proc = Process.Start(psi))
                {
                    proc?.WaitForExit();
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"eSpeak error: {e.Message}");
            }
        });
    }
}
