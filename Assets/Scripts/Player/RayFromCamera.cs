using UnityEngine;

public class RayFromCamera : RayProvider
{
    [SerializeField] private Camera sourceCamera;

    public override Ray GetRay()
    {
        return new Ray(sourceCamera.transform.position, sourceCamera.transform.forward);
    }
}
