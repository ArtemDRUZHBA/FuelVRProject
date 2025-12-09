using UnityEngine;

[RequireComponent(typeof(RayProvider))]
public class PlayerInputHandler : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private float interactionRange = 5f;
    [SerializeField] private LayerMask interactableMask;   // выставить в инспекторе

    private RayProvider rayProvider;

    private void Awake()
    {
        rayProvider = GetComponent<RayProvider>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E))
            return;

        // посылаем луч
        Ray ray = rayProvider.GetRay();
        Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.green, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactableMask))
        {
            Debug.Log("[Player] Hit " + hit.collider.name);

            // если на объекте есть IInteractable Ч вызываем
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
                return;
            }

            // иначе, если это машина Ч пробуем достучатьс€ до FuelSpot
           /* if (hit.collider.TryGetComponent<CarMovement>(out var carMov)
                && carMov.currentFuelStation != null)
            {
                Debug.Log("[Player] Activating UI via FuelSpot");
                carMov.currentFuelStation.Interact();
                return;
            }*/
        }
        else
        {
            Debug.Log("[Player] Ќичего не попало");
        }
    }
}
