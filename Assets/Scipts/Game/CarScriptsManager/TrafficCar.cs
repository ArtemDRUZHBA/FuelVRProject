using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    public LaneSetting[] spawnPoints;
    public CarObjectPool carPool;

    public List<GameObject> lanePaths;
    public List<GameObject> fuelSpots;
    public List<GameObject> fuelSpotEnterPaths;
    public List<GameObject> fuelSpotExitPaths;
    public List<GameObject> finalPaths;

    void Start()
    {
        foreach (LaneSetting spawnPoint in spawnPoints)
        {
            StartCoroutine(SpawnCar(spawnPoint));
        }
    }

    IEnumerator SpawnCar(LaneSetting spawnPoint)
    {
        while (spawnPoint.canSpawn)
        {
            yield return new WaitForSeconds(Random.Range(spawnPoint.minSpawnTime, spawnPoint.maxSpawnTime));

            if (spawnPoint.spawnPoint.name == "SpawnPoint2")
            {
                GameObject freeSpot = GetFreeFuelSpot();
                if (freeSpot == null)
                {
                    Debug.LogWarning("Нет свободных колонок для заправки! Ждем, пока колонка освободится...");
                    yield return new WaitForSeconds(2f);
                    continue; // вместо yield break – продолжаем проверять наличие свободной колонки
                }

                int index = fuelSpots.IndexOf(freeSpot);
                if (index < 0 || index >= fuelSpotEnterPaths.Count || index >= fuelSpotExitPaths.Count)
                {
                    Debug.LogError("Некорректные индексы путей для заправки.");
                    continue;
                }

                // Резервируем колонку сразу, чтобы она не выбиралась повторно
                FuelSpotController spotController = freeSpot.GetComponent<FuelSpotController>();
                spotController.ReserveSpot();
                // Для каждой колонки свой индивидуальный exit путь, а для финального пути – общий (finalPaths[0])
                if (finalPaths.Count == 0)
                {
                    Debug.LogError("Общий финальный путь не задан!");
                    continue;
                }
                spotController.SetPaths(fuelSpotExitPaths[index], finalPaths[0]);

                // Спавним машину
                GameObject car = carPool.GetCar(spawnPoint.spawnPoint.position);
                CarMovement carMovement = car.GetComponent<CarMovement>();
                // Задаем машине маршрут: сначала путь к зоне заправки, затем путь заезда к конкретной колонке
                carMovement.AddPath(lanePaths[1]);
                carMovement.AddPath(fuelSpotEnterPaths[index]);
                carMovement.StartMovement();
            }
            else  // SpawnPoint1 – обычное движение
            {
                GameObject car = carPool.GetCar(spawnPoint.spawnPoint.position);
                CarMovement carMovement = car.GetComponent<CarMovement>();
                carMovement.AddPath(lanePaths[0]);
                carMovement.StartMovement();
            }
        }
    }

    public GameObject GetFreeFuelSpot()
    {
        foreach (GameObject spot in fuelSpots)
        {
            FuelSpotController fsc = spot.GetComponent<FuelSpotController>();
            if (!fsc.IsOccupied() && !fsc.IsReserved())
            {
                return spot;
            }
        }
        return null;
    }
}
