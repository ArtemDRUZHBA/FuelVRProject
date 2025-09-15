using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestAnim : MonoBehaviour
{
    private Animator animator;
    private bool isOpen;

    public Material normal, selected;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HoverEnter() { GetComponent<MeshRenderer>().material = selected; }
    public void HoverExit() { GetComponent<MeshRenderer>().material = normal; }

    public void OpenOrClose()
    {
        Debug.Log("A");
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
