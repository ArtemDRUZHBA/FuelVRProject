using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Extinguish : MonoBehaviour
{
    [Header("Voice Keywords")]
    [SerializeField] private string[] _keywords = { "потуши", "туши", "убери", "уберай" };

    [Header("Animator")]
    [SerializeField] private Animator _animator;

    [Header("Animation Names")]
    [SerializeField] private string _animStart;
    [SerializeField] private string _animLoop;
    [SerializeField] private string _animFinish;

    private VoiceAssistant _voiceAssistant;
    private bool _finishTriggered = false;
    private void Start()
    {
        _voiceAssistant = GetComponent<VoiceAssistant>();

        _voiceAssistant.OnTextRecognized += OnTextRecognized;

        _animator.Play(_animStart);
        Debug.Log("VoiceAssistant найден: " + (_voiceAssistant != null));

    }

    public void OnStartAnimationFinished()
    {
        _animator.Play(_animLoop);
    }


    private void OnDestroy()
    {
        if (_voiceAssistant != null)
            _voiceAssistant.OnTextRecognized -= OnTextRecognized;
    }

    private void OnTextRecognized(string text)
    {
        Debug.Log("Extinguish получил текст: " + text);
        CheckString(text);
    }

    private void CheckString(string text)
    {
        if (_finishTriggered)
            return;

        string t = text.ToLower().Trim();

        string[] words = t.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string w in words)
        {
            if (_keywords.Any(k => w.Contains(k)))
            {
                UnityEngine.Debug.Log("«апущена последн€€ анимаци€");
                _finishTriggered = true;

                FindObjectOfType<SmokeBreath>()?.StopSmoke();

                _animator.Play(_animFinish);
                return;
            }
        }
    }
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("SPACE pressed запускаю финальную анимацию вручную");

            // „тобы не запускать повторно
            if (_finishTriggered)
                return;

            _finishTriggered = true;

            // ќстанавливаем дым, как в обычном сценарии
            FindObjectOfType<SmokeBreath>()?.StopSmoke();

            // «апускаем финальную анимацию
            _animator.Play(_animFinish);
        }
    }

}
