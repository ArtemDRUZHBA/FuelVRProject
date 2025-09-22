using UnityEngine;

public class FuelPuddleGrower : MonoBehaviour
{
    [Header("Рост лужи за попадание")]
    [SerializeField] private float growthPerHit = 2f;

    public void Grow()
    {
        var scale = transform.localScale;
        scale.x += growthPerHit * Time.deltaTime;
        scale.z += growthPerHit * Time.deltaTime;
        transform.localScale = scale;
    }
}
