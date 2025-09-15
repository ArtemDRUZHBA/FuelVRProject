using UnityEngine;

public class KeyboardAnimationTest : MonoBehaviour
{
    public Animator animator;
    public string animationStateName = "FuelHatch_MainAction";
    public KeyCode testKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(testKey))
        {
            Debug.Log(" лавиша нажата Ч запускаем анимацию");
            animator.Play(animationStateName, 0, 0f);
            animator.speed = 1f;
        }
    }
}
