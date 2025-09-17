using UnityEngine;

public class FuelPuddleGrower : MonoBehaviour
{
    [Header("Рост лужи за попадание")]
    [SerializeField] private float growthPerHit = 0.02f;

    public void Grow()
    {
        var scale = transform.localScale;
        scale.x += growthPerHit;
        scale.z += growthPerHit;
        transform.localScale = scale;
    }
}
