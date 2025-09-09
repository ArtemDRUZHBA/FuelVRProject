using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypointIndex = 0;

    public float speed = 25f;
    public float turnSpeed = 2f;
    public float closeEnoughDistance = 3.5f;

    public void AddPath(Transform[] newWaypoints)
    {
        foreach (Transform newWaypoint in newWaypoints)
        {
            waypoints.Add(newWaypoint);
        }
    }

    public void StartMovement()
    {
        if (waypoints.Count > 0)
        {
            MoveAlongPath();
        }
    }

    public void MoveAlongPath()
    {
        while (currentWaypointIndex < waypoints.Count)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];

            Rotate(targetWaypoint);
            float angle = Vector3.Angle(transform.forward, (targetWaypoint.position - transform.position).normalized);
            if (angle <= 90f)
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

            if (transform.position == targetWaypoint.position)
            {
                currentWaypointIndex++;
                Debug.Log(currentWaypointIndex + " " + transform.name);
            }
        }

        currentWaypointIndex = 0;
    }

    private void Rotate(Transform target)
    {
        Vector3 targetVector = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetVector.x, 0, targetVector.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }
}
