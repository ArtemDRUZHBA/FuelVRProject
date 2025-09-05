using UnityEngine;

public class VR_NozzleInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Nozzle nozzle;
    [SerializeField] private Transform rightHandTransform; // Transform ������� VR-�����������

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

        if (rightHandTransform == null)
        {
            TryAutoFindRightHand();
        }

        if (rightHandTransform == null)
        {
            Debug.LogWarning($"{name}: RightHandTransform �� �������� (VR). ����� ������ ���������� �������.");
            return;
        }

        nozzle.AttachTo(rightHandTransform);
    }

    private void TryAutoFindRightHand()
    {
        // ������� ����� �� �������� ��������� � XR Origin
        var candidates = new[] { "RightHand", "Right Hand", "RightHand Controller", "Right Controller", "XR RightHand Controller" };
        foreach (var c in candidates)
        {
            var go = GameObject.Find(c);
            if (go != null)
            {
                rightHandTransform = go.transform;
                Debug.Log($"[VR] ����� ������ ����������: {go.name}");
                return;
            }
        }
    }

    private void Reset()
    {
        if (nozzle == null) nozzle = GetComponentInParent<Nozzle>();
    }
}
