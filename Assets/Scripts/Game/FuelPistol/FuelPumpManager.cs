using UnityEngine;

public class FuelPumpManager : MonoBehaviour
{
    [SerializeField] private CreateFuelPistol _instFuelPistolAndHose;
    private void Start()
    {
        _instFuelPistolAndHose.GetComponent<CreateFuelPistol>();

        _instFuelPistolAndHose.InstantiateAndSaveFuelPistol();
    }
}
