using UnityEngine;

public class NozzleTrigger : MonoBehaviour
{
    [Header("���� ��� �������")]
    [SerializeField] private string fuelSocketTag = "FuelSocket";
    [SerializeField] private string fuelTankTag = "FuelTank";

    [Header("����� �������")]
    [SerializeField] private Transform socketInsertPoint;
    [SerializeField] private Transform tankInsertPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(fuelSocketTag) && socketInsertPoint != null)
        {
            SnapTo(socketInsertPoint);
            Debug.Log("�������� ��������� � ������");
        }

        else if (other.CompareTag(fuelTankTag) && tankInsertPoint != null)
        {
            SnapTo(tankInsertPoint);
            Debug.Log("�������� �������� � ���");
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
