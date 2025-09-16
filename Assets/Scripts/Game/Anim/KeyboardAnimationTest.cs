using UnityEngine;

public class KeyboardAnimationTest : MonoBehaviour
{
    [Header("Анимация")]
    public Animator animator;
    public string boolParameter = "IsOpen";

    [Header("Клавиша активации")]
    public KeyCode activationKey = KeyCode.E;

    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(activationKey))
        {
            isOpen = !isOpen;
            animator.SetBool(boolParameter, isOpen);
            Debug.Log("Клавиша нажата. IsOpen = " + isOpen);
        }
    }
}
