using UnityEngine;

public class CarInteract : MonoBehaviour, IInteractable
{
    private CarMovement carMovement;

    private void Awake()
    {
        carMovement = GetComponent<CarMovement>();
    }

    public void Interact()
    {
        
    }
}
