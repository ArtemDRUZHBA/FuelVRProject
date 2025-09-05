using UnityEngine;

public interface INuzzle : IInteractable
{
    void AttachTo(Transform targetHand);
    void ReturnToRest();
    bool IsHeld { get; }
}
