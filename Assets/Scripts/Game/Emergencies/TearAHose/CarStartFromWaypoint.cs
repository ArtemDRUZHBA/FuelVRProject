using UnityEngine;

public class CarStartFromWaypoint : MonoBehaviour
{
    [SerializeField] private CarMovement carMovement;
    [SerializeField] private int startIndex = 4; // с какого waypoint начинать

    private void Start()
    {
        if (carMovement == null)
            carMovement = GetComponent<CarMovement>();

        if (carMovement != null && carMovement.waypoints.Count > startIndex)
        {
            print("Разрешаем машине ехать");
            carMovement.currentWaypointIndex = startIndex;
            carMovement.canMove = true; // сразу разрешаем движение
        }
        else
        {
            Debug.LogWarning("CarStartFromWaypoint: индекс больше количества точек или CarMovement не найден!");
        }
    }
}
