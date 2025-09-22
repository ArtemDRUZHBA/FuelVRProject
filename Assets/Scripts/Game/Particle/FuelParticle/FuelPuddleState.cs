using UnityEngine;

public class FuelPuddleState : MonoBehaviour
{
    [SerializeField] private float threshold = 0.8f; // порог покрыти€

    private void OnCollisionStay(Collision collision)
    {
        // ѕровер€ем, что столкнулись с песчаной кучкой
        if (collision.gameObject.layer == LayerMask.NameToLayer("SandMark"))
        {
            Collider fuelCol = GetComponent<Collider>();
            Collider sandCol = collision.collider;

            if (IsCoveredEnough(fuelCol, sandCol, threshold))
            {
                Debug.Log("Ћужа засыпана песком 80%");
                Destroy(gameObject);
            }
        }
    }

    private bool IsCoveredEnough(Collider fuelCollider, Collider sandCollider, float threshold)
    {
        Bounds fuel = fuelCollider.bounds;
        Bounds sand = sandCollider.bounds;

        float xOverlap = Mathf.Max(0, Mathf.Min(fuel.max.x, sand.max.x) - Mathf.Max(fuel.min.x, sand.min.x));
        float zOverlap = Mathf.Max(0, Mathf.Min(fuel.max.z, sand.max.z) - Mathf.Max(fuel.min.z, sand.min.z));

        float overlapArea = xOverlap * zOverlap;
        float fuelArea = fuel.size.x * fuel.size.z;

        if (fuelArea <= 0) return false;

        float coverage = overlapArea / fuelArea;
        return coverage >= threshold;
    }
}
