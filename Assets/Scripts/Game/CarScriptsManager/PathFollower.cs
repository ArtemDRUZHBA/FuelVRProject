/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public List<Transform> pathPoints;
    public float speed = 5f;
    private int currentPointIndex = 0;
    private bool refueling = false;
    private Transform fuelSpot;
    private GameObject exitPath;
    private GameObject finalPath;
    private CarObjectPool carObjectPool;

    public void SetPath(GameObject path)
    {
        pathPoints = new List<Transform>();
        foreach (Transform point in path.transform)
        {
            pathPoints.Add(point);
        }
        currentPointIndex = 0;
    }

    public void SetFuelSpot(Transform spot, GameObject exit, GameObject final, CarObjectPool pool)
    {
        fuelSpot = spot;
        exitPath = exit;
        finalPath = final;
        carObjectPool = pool;
    }

    private void Update()
    {
        if (refueling) return;

        if (currentPointIndex < pathPoints.Count)
        {
            MoveTowardsPoint();
        }
        else if (fuelSpot != null)
        {
            StartCoroutine(Refuel());
        }
        else if (finalPath != null) // Едем на последний путь
        {
            SetPath(finalPath);
            finalPath = null; // Очищаем переменную, чтобы не зациклилось
        }
        else
        {
            ReturnToPool();
        }
    }

    private void MoveTowardsPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, pathPoints[currentPointIndex].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pathPoints[currentPointIndex].position) < 0.2f)
        {
            currentPointIndex++;
        }
    }

    private IEnumerator Refuel()
    {
        refueling = true;
        yield return new WaitForSeconds(5f);

        fuelSpot.GetComponent<FuelSpotController>().isOccupied = false;
        SetPath(exitPath);
        currentPointIndex = 0;
        refueling = false;
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
*/