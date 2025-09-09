using System.Collections.Generic;
using UnityEngine;

public class CarObjectPool : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public int poolSize = 10;
    public Transform carParentTransform;
    private List<GameObject> carPool = new List<GameObject>();

    private GameObject CreateCar(Vector3 spawnPosition)
    {
        GameObject newCar = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)]);
        newCar.transform.parent = carParentTransform;
        newCar.SetActive(false);
        return newCar;
    }

    public GameObject GetCar(Vector3 spawnPosition)
    {
        foreach (GameObject car in carPool)
        {
            if (!car.activeInHierarchy)
            {
                car.SetActive(true);
                return car;
            }
        }

        if (carPool.Count < poolSize)
        {
            GameObject newCar = CreateCar(spawnPosition);
            carPool.Add(newCar);
            return newCar;
        }
        
        return null;
    }
}
