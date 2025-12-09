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
        if (animator == null)
            animator = GetComponentInParent<Animator>();
    }
    public void ToggleAnimation()
    {
        Debug.Log($"ToggleAnimation called on {gameObject.name}; animator = {animator}", this);
        isOpen = !isOpen;
        animator.SetBool(boolParameter, isOpen);
        Debug.Log("IsOpen set to: " + isOpen);
    }
}
