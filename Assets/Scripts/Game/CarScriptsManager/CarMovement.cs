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

    public bool canMove;
    private bool isFueling;
    private bool stoppedAtGazStation;

    private void OnEnable()
    {
        isFueling = false;
        stoppedAtGazStation = false;
        currentWaypointIndex = 0;

    }

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
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }

    private void FixedUpdate()
    {
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        if (!canMove)
            return; 
        CheakWaypoint();

        if (!isFueling)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];

            Rotate(targetWaypoint);
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

            if (transform.position == targetWaypoint.position)
            {
                currentWaypointIndex++;
            }
        }
    }

    public void StopFueling()
    {
        canMove = true;
        isFueling = false;
    }

    private void CheakWaypoint()
    {
        if (currentWaypointIndex == 0)
            return;

        if (waypoints[currentWaypointIndex - 1].TryGetComponent(out FuelSpotController fsc) && !stoppedAtGazStation)
        {
            canMove = false;
            isFueling = true;
            stoppedAtGazStation = true;
        }
    }

    private void Rotate(Transform target)
    {
        Vector3 targetVector = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetVector.x, 0, targetVector.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }
}
