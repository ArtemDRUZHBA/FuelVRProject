using System.Collections;
using UnityEngine;

public class FuelSpotController : MonoBehaviour
{
    private bool isOccupied = false;

    public bool TryOccupySpot()
    {
        if (isOccupied)
        {
            return false;
        }
        
        return true;
    }
    public void OccupyFuelSpot()
    {
        isOccupied = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isOccupied = false;
    }
}
