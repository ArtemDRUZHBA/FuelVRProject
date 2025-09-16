using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class XRTriggerAnimator : MonoBehaviour
{
    public Animator animator;
    public string boolParameter = "IsOpen";

    private bool isOpen = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleAnimation()
    {
        isOpen = !isOpen;
        animator.SetBool(boolParameter, isOpen);
    }
}
