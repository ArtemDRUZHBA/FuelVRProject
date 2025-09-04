using UnityEngine;
public class RayProvider : MonoBehaviour
{
    public Ray GetRay() => new Ray(transform.position, transform.forward);
}
