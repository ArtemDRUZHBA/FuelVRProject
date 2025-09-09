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
        
        isOccupied = true;
        return true;
    }

    public void UnOccupySpot()
    {
        isOccupied = false;
    }
}
