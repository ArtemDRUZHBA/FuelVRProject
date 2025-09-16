using UnityEngine;

public class AnimationFuelTank : MonoBehaviour
{
    public bool isOpen;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
