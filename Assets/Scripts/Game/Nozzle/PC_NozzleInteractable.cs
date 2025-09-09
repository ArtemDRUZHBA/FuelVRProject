using UnityEngine;

public class PC_NozzleInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Nozzle nozzle;
    [SerializeField] private Transform handTransform; // пустышка-рука (Anchor под камерой)

    public void Interact()
    {
        if (nozzle == null)
        {
            Debug.LogWarning($"{name}: Nozzle не назначен");
            return;
        }

        if (nozzle.IsHeld)
        {
            nozzle.ReturnToRest();
            return;
        }

        if (handTransform == null)
        {
            Debug.LogWarning($"{name}: HandTransform не назначен (ПК). Создай пустышку-руку и укажи её.");
            return;
        }

        nozzle.AttachTo(handTransform);
    }

    private void Reset()
    {
        if (nozzle == null) nozzle = GetComponentInParent<Nozzle>();
    }
}
