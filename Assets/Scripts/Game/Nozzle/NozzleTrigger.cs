using UnityEngine;

public class NozzleTrigger : MonoBehaviour
{
    [Header("Теги для вставки")]
    [SerializeField] private string fuelSocketTag = "FuelSocket";
    [SerializeField] private string fuelTankTag = "FuelTank";

    [Header("Точки вставки")]
    [SerializeField] private Transform socketInsertPoint;
    [SerializeField] private Transform tankInsertPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(fuelSocketTag) && socketInsertPoint != null)
        {
            SnapTo(socketInsertPoint);
            Debug.Log("Пистолет возвращён в гнездо");
        }

        else if (other.CompareTag(fuelTankTag) && tankInsertPoint != null)
        {
            SnapTo(tankInsertPoint);
            Debug.Log("Пистолет вставлен в бак");
        }
    }

    private void SnapTo(Transform target)
    {
        transform.SetParent(target);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        var rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        //var col = GetComponent<Collider>();
        //if (col != null) col.enabled = false;
    }
}
