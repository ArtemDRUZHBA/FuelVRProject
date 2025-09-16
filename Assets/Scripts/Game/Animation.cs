using UnityEngine;

public class Animation : MonoBehaviour
{
    public bool isOpen;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
