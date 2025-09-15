using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class XRTriggerAnimator : MonoBehaviour
{
    public Animator animator;
    public string boolParameter = "IsOpen";

    private bool isOpen = false;

    public void ToggleAnimation()
    {
        if (!animator) return;

        isOpen = !isOpen;
        animator.SetBool(boolParameter, isOpen);
        Debug.Log("IsOpen set to: " + isOpen);
    }
}
