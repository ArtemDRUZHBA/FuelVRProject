using UnityEngine;

public class FuelTank : MonoBehaviour
{
    private bool isOpen;
    public float maxFuelInTank;
    private float fuelInTankBeforeRefueling;
    public float fuelInTank;
    private bool isFull;

    public bool CanFueling() => isOpen;

    public void Fueling(float fuelingSpeed)
    {
        if (!isFull)
            fuelInTank += fuelingSpeed * Time.deltaTime;
    }

    private void Awake()
    {
        fuelInTankBeforeRefueling = Random.Range(3, maxFuelInTank - 10);
        fuelInTank = fuelInTankBeforeRefueling;
    }

    private void Update()
    {
        CheckHowFullTank();
    }

    private void CheckHowFullTank()
    {
        if (fuelInTank >= maxFuelInTank)
        {
            isFull = true;
            fuelInTank = maxFuelInTank;

            GetComponentInParent<CarMovement>().StopFueling();
        }
    }
}
