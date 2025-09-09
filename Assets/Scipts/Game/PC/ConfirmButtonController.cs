using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButtonController : MonoBehaviour
{
    public enum Pump
    {
        First,
        Second
    }
    public enum FuelType
    {
        BadGasoline,
        Gasoline,
        Diesel
    }

    [Range(0, 100)]
    private float _fuelCount;
    private Pump _pump;
    private FuelType _fuelType;

    public void SetFuelCount(int fuelCount) { _fuelCount = fuelCount; }
    public void SetPump(int index)
    {
        switch (index)
        {
            case 0:
                _pump = Pump.First;
                break;
            case 1:
                _pump = Pump.Second;
                break;
        }
    }
    public void SetFuelType(int index)
    {
        switch (index)
        {
            case 0:
                _fuelType = FuelType.BadGasoline;
                break;
            case 1:
                _fuelType = FuelType.Gasoline;
                break;
            case 2:
                _fuelType = FuelType.Diesel;
                break;
        }
    }

    public void Confirm()
    {
        if (_fuelCount == 0)
        {
            return;
        }

            Debug.Log(_pump + " " + _fuelType + " " + _fuelCount);
        // отправить на колонку данные
    }
}
