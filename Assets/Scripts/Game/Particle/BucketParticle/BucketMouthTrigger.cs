using UnityEngine;

public class BucketMouthTrigger : MonoBehaviour
{
    public bool IsTouchingSandSource { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SandSource"))
            IsTouchingSandSource = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SandSource"))
            IsTouchingSandSource = false;
    }
}
