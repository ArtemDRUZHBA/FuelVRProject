using UnityEngine;

public class CarInteract : MonoBehaviour, IInteractable
{
    private CarMovement carMov;

    private void Awake()
    {
        carMov = GetComponent<CarMovement>();
    }

    public void Interact()
    {
        if (carMov != null && carMov.currentFuelStation != null)
        {
            carMov.currentFuelStation.Interact();
        }
    }
}
