using UnityEngine;

public class Nurbs : MonoBehaviour
{
    private NozzleTrigger nozzle;

    private void Start()
    {
        nozzle = GetComponentInParent<NozzleTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FuelTank fuelTank))
        {
            nozzle.SetFuelTank(fuelTank);
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out FuelTank fuelTank))
        {
            nozzle.SetFuelTank(fuelTank);
            Debug.Log("Exit");
        }
    }
}
