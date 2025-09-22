using UnityEditor.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine;

public class PuddleMerger : MonoBehaviour
{
    [Header("Слои, в которых разрешено объединение")]
    [SerializeField] private string fuelLayerName = "Puddle";   // слой для луж топлива
    [SerializeField] private string sandLayerName = "SandMark"; // слой для луж песка

    private int fuelLayer;
    private int sandLayer;

    private void Awake()
    {
        // Кэшируем индексы слоёв по именам
        fuelLayer = LayerMask.NameToLayer(fuelLayerName);
        sandLayer = LayerMask.NameToLayer(sandLayerName);
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out FuelPuddleGrower puddle) && puddle.gameObject.layer == gameObject.layer)
        {
            Vector3 myScale = transform.localScale;
            Vector3 otherScale = puddle.transform.localScale;

            if (myScale.x >= otherScale.x)
            {
                Destroy(puddle.gameObject);
                Vector3 scale = transform.localScale;
                float add = otherScale.x / 2f;
                scale.x += add;
                scale.z += add;
                transform.localScale = scale;
            }
            else
            {
                return;
            }
        }
    }
}
