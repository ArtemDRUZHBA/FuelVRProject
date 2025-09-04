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
                    Debug.LogWarning("��� ��������� ������� ��� ��������! ����, ���� ������� �����������...");
                    yield return new WaitForSeconds(2f);
                    continue; // ������ yield break � ���������� ��������� ������� ��������� �������
                }

                int index = fuelSpots.IndexOf(freeSpot);
                if (index < 0 || index >= fuelSpotEnterPaths.Count || index >= fuelSpotExitPaths.Count)
                {
                    Debug.LogError("������������ ������� ����� ��� ��������.");
                    continue;
                }

                // ����������� ������� �����, ����� ��� �� ���������� ��������
                FuelSpotController spotController = freeSpot.GetComponent<FuelSpotController>();
                spotController.ReserveSpot();
                // ��� ������ ������� ���� �������������� exit ����, � ��� ���������� ���� � ����� (finalPaths[0])
                if (finalPaths.Count == 0)
                {
                    Debug.LogError("����� ��������� ���� �� �����!");
                    continue;
                }
                spotController.SetPaths(fuelSpotExitPaths[index], finalPaths[0]);

                // ������� ������
                GameObject car = carPool.GetCar(spawnPoint.spawnPoint.position);
                CarMovement carMovement = car.GetComponent<CarMovement>();
                // ������ ������ �������: ������� ���� � ���� ��������, ����� ���� ������ � ���������� �������
                carMovement.AddPath(lanePaths[1]);
                carMovement.AddPath(fuelSpotEnterPaths[index]);
                carMovement.StartMovement();
            }
            else  // SpawnPoint1 � ������� ��������
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
