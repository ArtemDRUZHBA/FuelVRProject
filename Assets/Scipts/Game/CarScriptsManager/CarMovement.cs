using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private Queue<List<Transform>> pathQueue = new Queue<List<Transform>>();
    private List<Transform> currentPath;
    private int currentWaypointIndex = 0;

    public float speed = 25f;
    public float turnSpeed = 2f;    
    public float accelerationForce = 25f;
    public float closeEnoughDistance = 3.5f; 

    private Rigidbody rb;
    private Vector3 targetDelta;
    public FuelSpotController currentFuelStation;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
        }
    }

    // Добавляем путь в очередь
    public void AddPath(GameObject path)
    {
        if (path == null)
        {
            return;
        }
        List<Transform> newPath = new List<Transform>();
        foreach (Transform child in path.transform)
        {
            newPath.Add(child);
        }
        pathQueue.Enqueue(newPath);
    }

    // Запускаем движение, если в очереди есть пути
    public void StartMovement()
    {
        if (pathQueue.Count > 0)
        {
            StartCoroutine(MoveAlongPath());
        }
    }

    public IEnumerator MoveAlongPath()
    {
        while (pathQueue.Count > 0)
        {
            currentPath = pathQueue.Dequeue();
            currentWaypointIndex = 0;

            while (currentWaypointIndex < currentPath.Count)
            {
                Transform targetWaypoint = currentPath[currentWaypointIndex]; 
                while (Vector3.Distance(transform.position, targetWaypoint.position) > 3f)
                {
                    Rotate(targetWaypoint);
                    float angle = Vector3.Angle(transform.forward, (targetWaypoint.position - transform.position).normalized);
                    if (angle <= 90f)
                        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
                        yield return null;
                }
                currentWaypointIndex++;
            }
        }
        yield break;
    }

    // Метод плавного поворота: интерполирует текущую ротацию к целевому направлению
    private void Rotate(Transform target)
    {
        Vector3 targetVector = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetVector.x, 0, targetVector.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }
}
