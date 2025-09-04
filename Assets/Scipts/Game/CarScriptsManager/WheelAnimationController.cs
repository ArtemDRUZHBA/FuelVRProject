using UnityEngine;

public class WheelAnimationController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 300f;
    public Transform[] wheels;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
    }
}
