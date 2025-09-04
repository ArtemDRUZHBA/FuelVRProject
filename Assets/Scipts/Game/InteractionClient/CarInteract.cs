using UnityEngine;

public class CarInteract : MonoBehaviour, IInteractable
{
    private CarMovement carMov;

    private void Awake()
    {
        carMov = GetComponent<CarMovement>();
    }

    // этот метод будет вызван, когда игрок целится и жмёт E по машине
    public void Interact()
    {
        if (carMov != null && carMov.currentFuelStation != null)
        {
            Debug.Log("[CarInteract] Forwarding Interact to FuelSpot");
            carMov.currentFuelStation.Interact();
        }
        else
        {
            Debug.Log("[CarInteract] Нет станции для заправки");
        }
    }
}
