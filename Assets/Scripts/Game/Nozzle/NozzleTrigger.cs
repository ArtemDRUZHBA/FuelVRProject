using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class NozzleTrigger : MonoBehaviour
{
    [Header("���� ��� �������")]
    [SerializeField] private string fuelSocketTag = "FuelSocket";
    [SerializeField] private string fuelTankTag = "FuelTank";

    [Header("����� �������")]
    [SerializeField] private Transform socketInsertPoint;
    [SerializeField] private Transform tankInsertPoint;

    [SerializeField] private XRInteractionManager interactionManager;
    [SerializeField] private XRGrabInteractable grabInteractable;

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
        var interactor = grabInteractable.firstInteractorSelecting;
        if (interactor != null)
        {
            interactionManager.SelectExit(interactor, grabInteractable);
        }

        transform.SetParent(target);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
