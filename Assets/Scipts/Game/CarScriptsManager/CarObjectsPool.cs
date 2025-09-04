using System.Collections.Generic;
using UnityEngine;

public class CarObjectPool : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public int poolSize = 10;
    private List<GameObject> carPool = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject car = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)]);
            car.SetActive(false);
            carPool.Add(car);
        }
    }

    public GameObject GetCar(Vector3 spawnPosition)
    {
        foreach (GameObject car in carPool)
        {
            if (!car.activeInHierarchy)
            {
                car.transform.position = spawnPosition;
                car.SetActive(true);
                return car;
            }
        }

        GameObject newCar = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], spawnPosition, Quaternion.identity);
        carPool.Add(newCar);
        return newCar;
    }
}
