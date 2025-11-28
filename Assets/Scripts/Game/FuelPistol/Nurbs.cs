using UnityEngine;

public class Nurbs : MonoBehaviour
{
    private FuelPistolTrigger _fuelPistol;

    private void Start()
    {
        _fuelPistol = GetComponentInParent<FuelPistolTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FuelTank fuelTank))
        {
            _fuelPistol.SetFuelTank(fuelTank);
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out FuelTank fuelTank))
        {
            _fuelPistol.SetFuelTank(fuelTank);
            Debug.Log("Exit");
        }
    }
}
