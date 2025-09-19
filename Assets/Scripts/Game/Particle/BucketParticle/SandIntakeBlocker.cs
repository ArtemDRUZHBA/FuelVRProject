using UnityEngine;

public class SandIntakeBlocker : MonoBehaviour
{
    public bool IsInsideIntakeZone { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SandIntakeZone"))
            IsInsideIntakeZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SandIntakeZone"))
            IsInsideIntakeZone = false;
    }
}
