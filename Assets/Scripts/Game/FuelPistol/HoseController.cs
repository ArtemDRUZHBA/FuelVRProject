using UnityEngine;

public class HoseController : MonoBehaviour
{
    public Transform[] bones;       // кости шланга
    public Transform anchorPoint;   // точка крепления (начало)
    public Transform hoseEndPoint;  // точка выхода из пистолета/колонки

    private Rigidbody firstRb;
    private Rigidbody lastRb;

    void Start()
    {
        if (bones.Length > 0)
        {
            firstRb = bones[0].GetComponent<Rigidbody>();
            lastRb = bones[bones.Length - 1].GetComponent<Rigidbody>();

            // фиксируем первую кость
            firstRb.isKinematic = true;
            bones[0].position = anchorPoint.position;
        }
    }

    void FixedUpdate()
    {
        if (lastRb != null && hoseEndPoint != null)
        {
            // двигаем последнюю кость к пистолету
            lastRb.MovePosition(hoseEndPoint.position);
        }
    }
}
