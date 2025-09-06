using UnityEngine;

public class PC_NozzleInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Nozzle nozzle;
    [SerializeField] private Transform handTransform; // ��������-���� (Anchor ��� �������)

    public void Interact()
    {
        if (nozzle == null)
        {
            Debug.LogWarning($"{name}: Nozzle �� ��������");
            return;
        }

        if (nozzle.IsHeld)
        {
            nozzle.ReturnToRest();
            return;
        }

        if (handTransform == null)
        {
            Debug.LogWarning($"{name}: HandTransform �� �������� (��). ������ ��������-���� � ����� �.");
            return;
        }

        nozzle.AttachTo(handTransform);
    }

    private void Reset()
    {
        if (nozzle == null) nozzle = GetComponentInParent<Nozzle>();
    }
}
