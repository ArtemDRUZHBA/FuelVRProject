using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class NozzleTrigger : MonoBehaviour
{
    public bool inHand;
    public bool isFueling;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out FuelSocket fs) && !inHand)
        {
            transform.position = fs.transform.position;
            transform.rotation = fs.transform.rotation;
        }
        else if (other.TryGetComponent(out FuelTank ft) && !inHand)
        {
            transform.position = ft.transform.position;
            transform.rotation = ft.transform.rotation; 
            isFueling = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out FuelTank ft))
        {
            isFueling = false;
        }
    }

    private void FixedUpdate()
    {
        Fueling();
    }

    private void Fueling()
    {
        if (!isFueling)
            return;
        Debug.Log("Fueling Started");
        // Заправлять
    }

    public void NozzleTaked()
    {
        inHand = true;
        isFueling = false;
    }

    public void NozzleThrowned()
    {
        inHand = false;
        isFueling = false;
    }

    public void StartFueling()
    {
        if (inHand && !isFueling)
            isFueling = true;
    }

    public void StopFueling()
    {
        if (inHand && isFueling)
            isFueling = false;
    }
}
