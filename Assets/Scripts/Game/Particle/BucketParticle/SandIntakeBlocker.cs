using UnityEngine;

public class SandIntakeBlocker : MonoBehaviour
{
    public bool IsInsideIntakeZone { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SandIntakeZone"))
        {
            IsInsideIntakeZone = true;
            Debug.Log("Вошёл в запретную зону");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SandIntakeZone"))
        {
            IsInsideIntakeZone = false;
            Debug.Log("Вышел из запретной зоны");
        }
    }
}
