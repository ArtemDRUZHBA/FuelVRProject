using UnityEngine;

public class NozzleTrigger : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] private Nozzle _nozzle;
    [SerializeField] private Collider _fuelSocketTrigger;
    [SerializeField] private Collider _nozzleHandleTrigger;

    [Header("XR Snap Targets")]
    [SerializeField] private Transform handTarget;
    [SerializeField] private Transform tankTarget;
    [SerializeField] private Transform socketTarget;

    [Header("XR Snap Parents")]
    [SerializeField] private Transform handParent;
    [SerializeField] private Transform tankParent;
    [SerializeField] private Transform socketParent;

    [Header("Snapper")]
    [SerializeField] private XRObjectSnapper snapper;

    private void Start()
    {
        _fuelSocketTrigger.isTrigger = false;
        _nozzleHandleTrigger.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NozzleHandle"))
        {
            Debug.Log("�������� ���� ��������");
            _fuelSocketTrigger.isTrigger = true;
            _nozzleHandleTrigger.isTrigger = false;

            transform.SetParent(other.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        else if (other.CompareTag("FuelSocket"))
        {
            Debug.Log("�������� ������ �������� � ������");
            _fuelSocketTrigger.isTrigger = false;
            _nozzleHandleTrigger.isTrigger = true;

            transform.SetParent(other.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        else if (other.CompareTag("FuelTank"))
        {
            Debug.Log("�������� ������� �������� � ���");
            _nozzleHandleTrigger.isTrigger = true;
            //snapper.SnapObject(tankTarget, tankParent);
        }
    }
}
