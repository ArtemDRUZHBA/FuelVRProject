using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TestAnim : MonoBehaviour
{
    public InputActionReference leftTrigger, rightTrigger;
    public InputActionReference mouseClick;
    public Transform leftController, rightController;
    public float interactDistance;

    public void OnLeftInteract(Transform controllerTransform)
    {
        Physics.Raycast(controllerTransform.position, leftController.forward, out RaycastHit hitInfo, interactDistance);
        if (hitInfo.transform.gameObject.TryGetComponent(out Animation animation))
            ToggleAnimation(animation.animator, animation.isOpen);
    }
    public void OnRightInteract(Transform controllerTransform)
    {
        Physics.Raycast(controllerTransform.position, leftController.forward, out RaycastHit hitInfo, interactDistance);
        if (hitInfo.transform.gameObject.TryGetComponent(out Animation animation))
            ToggleAnimation(animation.animator, animation.isOpen);
    }

    private void ToggleAnimation(Animator animator, bool isOpen)
    {
        if (isOpen)
        {
            isOpen = !isOpen;
            animator.SetBool("isOpen", true);
        }
        else if (!isOpen)
        {
            isOpen = !isOpen;
            animator.SetBool("isOpen", false);
        }
    }
}
