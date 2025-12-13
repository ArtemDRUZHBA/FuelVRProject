using System.Linq;
using UnityEngine;

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

        if (_keywords.Any(word => text.ToLower().Contains(word)))
        {
            _finishTriggered = true;
            _animator.Play(_animFinish);
        }
    }
}
