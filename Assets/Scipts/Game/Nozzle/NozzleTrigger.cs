using UnityEngine;

public class NozzleTrigger : MonoBehaviour
{
    [SerializeField] private Nozzle _nozzle;
    [SerializeField] private Collider _fuelSocketTrigger;
    [SerializeField] private Collider _nozzleHandleTrigger;

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
            //����� ����������� ������ XR � �������, � ������ ������� ����� ���������
        }
        if (other.CompareTag("FuelSocket"))
        {
            Debug.Log("�������� ������ �������� � ������");
            _fuelSocketTrigger.isTrigger = false;
            _nozzleHandleTrigger.isTrigger = true;
            _nozzle.ReturnToRest();
        }
        if (other.CompareTag("FuelTank"))
        {
            Debug.Log("�������� ������� �������� � ���");
            _nozzleHandleTrigger.isTrigger = true;
            //����� ������ � ����������� ��������� � �������� ������� ����� ���� � ���������� �������
        }
    }
}
