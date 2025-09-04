using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private Queue<List<Transform>> pathQueue = new Queue<List<Transform>>();
    private List<Transform> currentPath;
    private int currentWaypointIndex = 0;

    public float speed = 25f; // Не используется напрямую, но можно применять для масштабирования силы
    public float turnSpeed = 2f;         // Скорость поворота
    public float accelerationForce = 25f; // Сила ускорения
    public float closeEnoughDistance = 3.5f; // Расстояние, при котором считаем, что достигли точки

    private Rigidbody rb;
    private Vector3 targetDelta; // Направление к следующей точке
    public FuelSpotController currentFuelStation; // Ссылка на колонку, которая заправляет эту машину

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody не найден на " + gameObject.name);
        }
    }

    // Добавляем путь в очередь
    public void AddPath(GameObject path)
    {
        if (path == null)
        {
            Debug.LogError($"Ошибка: получен NULL путь для машины {gameObject.name}");
            return;
        }
        List<Transform> newPath = new List<Transform>();
        foreach (Transform child in path.transform)
        {
            newPath.Add(child);
        }
        pathQueue.Enqueue(newPath);
        Debug.Log($"Машина {gameObject.name} добавила путь `{path.name}`, {newPath.Count} точек.");
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
                    else
                    {
                        Debug.LogError("AAA");
                    }
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
