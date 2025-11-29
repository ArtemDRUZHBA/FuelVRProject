using UnityEngine;
using UnityEngine.Serialization;

public class HoseController : MonoBehaviour
{
    public Transform[] bones;     // кости шланга

    public Transform anchorPoint; // точка крепления (начало)
    public Transform hoseEndPoint;   // точка выхода из пистолета/колонки

    private void Update()
    {
        if (bones.Length > 0 && hoseEndPoint != null)
        {
            Rigidbody lastRb = bones[bones.Length - 1].GetComponent<Rigidbody>();
            lastRb.MovePosition(hoseEndPoint.position);
        }
    }
}
