using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            other.transform.parent.gameObject.SetActive(false);
        }
    }
}
