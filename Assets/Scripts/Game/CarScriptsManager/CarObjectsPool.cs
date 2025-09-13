using System.Collections.Generic;
using UnityEngine;

public class CarObjectPool : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public int poolSize = 10;
    public Transform carParentTransform;
    private List<GameObject> carPool = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (carPool.Count < poolSize)
            {
                CreateCar();
            }
        }
    }

    private void CreateCar()
    {
        GameObject newCar = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)]);
        newCar.transform.parent = carParentTransform;
        newCar.SetActive(false);
        carPool.Add(newCar);
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
        return null;
    }
}
