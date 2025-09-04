using UnityEngine;

public interface INozzle : IInteractable
{
    void AttachTo(Transform targetHand);
    void ReturnToRest();
    bool IsHeld { get; }
}
