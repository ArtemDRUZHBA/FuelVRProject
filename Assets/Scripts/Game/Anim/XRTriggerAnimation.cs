using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class XRTriggerAnimator : MonoBehaviour
{
    [Header("Анимация")] 
    public AnimatorTarget[] targets;
    private bool isOpen = false;

    public void ToggleAnimation()
    {
        foreach (var target in targets)
        {
            if (!target.animator || string.IsNullOrEmpty(target.animationStateName)) continue;

            if (!isOpen)
            {
                target.animator.Play(target.animationStateName, 0, 0f);
                target.animator.speed = 1f;
            }
            else
            {
                target.animator.Play(target.animationStateName, 0, 1f);
                target.animator.speed = -1f;
            }
        }

        isOpen = !isOpen;
    }
}
[System.Serializable]
public class AnimatorTarget
{
    public Animator animator;
    public string animationStateName;
}
