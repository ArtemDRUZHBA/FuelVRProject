using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    public CarObjectPool carPool;

    [Header("SpawnPoints")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Path1")]
    [SerializeField] private Transform[] path1;

    [Header("Path2")]
    [SerializeField] private Transform[] path2;
    [Header("Path3")]
    [SerializeField] private Transform[] path3;
    [Header("FuelSpots")]
    [SerializeField] private Transform[] fuelSpots;

    [Header("Preferences")]
    [SerializeField] private float minSpawnTime = 2f;
    [SerializeField] private float maxSpawnTime = 5f;
    [SerializeField] private float spawnChance = 1f;
    [SerializeField] private bool canSpawn = true;

    void Start()
    {
        StartCoroutine("SpawnCar");
    }

    IEnumerator SpawnCar()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnPointIndex];

        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

        if (spawnPointIndex == 0)
        {
            GameObject car = carPool.GetCar(spawnPoint.position);
            if (car != null)
            {
                CarMovement carMovement = car.GetComponent<CarMovement>();
                carMovement.AddPath(path1); 
                carMovement.StartMovement();
            }
        }
        else
        {
            int freeSpotIndex = TryGetFreeFuelSpot();
            if (freeSpotIndex != fuelSpots.Length)
            {
                GameObject car = carPool.GetCar(spawnPoint.position);
                if (car != null)
                {
                    CarMovement carMovement = car.GetComponent<CarMovement>();
                    fuelSpots[freeSpotIndex].GetComponent<FuelSpotController>().OccupyFuelSpot();

                    if (freeSpotIndex == 0)
                    {
                        carMovement.AddPath(path2);
                    }
                    else
                    {
                        carMovement.AddPath(path3);
                    }
                    
                    carMovement.StartMovement();
                }
            }
        }
        StartCoroutine("SpawnCar");
    }

    public int TryGetFreeFuelSpot()
    {
        for (int i = 0; i < fuelSpots.Length; i++)
        {
            FuelSpotController fsc = fuelSpots[i].GetComponent<FuelSpotController>();
            if (fsc.TryOccupySpot())
            {
                return i;
            }
        }
        return fuelSpots.Length;
    }
}
