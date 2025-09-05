using UnityEngine;

public class RayFromController : RayProvider
{
    [SerializeField] private Transform controllerTransform;

    public override Ray GetRay()
    {
        return new Ray(controllerTransform.position, controllerTransform.forward);
    }
}
