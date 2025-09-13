using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRTriggerAnimator : MonoBehaviour
{
    public XRController controller; // назначается в инспекторе
    public Animator animator;
    public string triggerName = "Open";

    void Update()
    {
        if (controller != null && controller.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool isPressed) && isPressed)
        {
            animator.SetTrigger(triggerName);
        }
    }
}
