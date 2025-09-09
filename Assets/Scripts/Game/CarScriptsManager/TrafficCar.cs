using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    public CarObjectPool carPool;

    [Header("SpawnPoints")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Path1")]
    [SerializeField] private Transform[] path1;

    [Header("Path2")]
    [SerializeField] private Transform[] startPath;
    [SerializeField] private Transform[] fuelSpots;
    [SerializeField] private Transform[] finalPath;

    [Header("Preferences")]
    [SerializeField] private float minSpawnTime = 2f;
    [SerializeField] private float maxSpawnTime = 5f;
    [SerializeField] private float spawnChance = 1f;
    [SerializeField] private bool canSpawn = true;

    void Start()
    {
        for (int i = 0; i < spawnPoints.Count(); i++)
        {
            StartCoroutine(SpawnCar(spawnPoints[i], i));
        }
    }

    IEnumerator SpawnCar(Transform spawnPoint, int waypointIndex)
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            if (waypointIndex == 0)
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
                Transform freeSpot = TryGetFreeFuelSpot();
                GameObject car = carPool.GetCar(spawnPoint.position);

                if (freeSpot != null && car != null)
                {
                    CarMovement carMovement = car.GetComponent<CarMovement>();

                    Transform[] path = new Transform[startPath.Length + 1 + finalPath.Length]; 

                    for (int i = 0; i < path.Length; i++)
                    {
                        if (i < startPath.Length)
                            path[i] = startPath[i];
                        if (i == startPath.Length)
                            path[i] = freeSpot;
                        else
                            path[i] = finalPath[0];
                    }

                    carMovement.AddPath(path);
                    carMovement.StartMovement();
                }
            }
        }
    }

    public Transform TryGetFreeFuelSpot()
    {
        foreach (Transform spot in fuelSpots)
        {
            FuelSpotController fsc = spot.GetComponent<FuelSpotController>();
            if (fsc.TryOccupySpot())
            {
                return spot;
            }
        }
        return null;
    }
}
