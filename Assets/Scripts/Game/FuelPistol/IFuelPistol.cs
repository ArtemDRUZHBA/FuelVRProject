using UnityEngine;

public interface IFuelPistol : IInteractable
{
    void AttachTo(Transform targetHand);
    void ReturnToRest();
    bool IsHeld { get; }
}
