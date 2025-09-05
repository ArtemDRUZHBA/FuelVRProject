using UnityEngine;

public class ColumnHoseSetup : MonoBehaviour
{
    public GameObject hosePrefab;      // Префаб шланга
    public int hoseCount = 8;          // Количество пистолетов

    void Start()
    {
        for (int i = 1; i <= hoseCount; i++)
        {
            Transform anchor = transform.FindDeepChild($"HoseStartPoint{i}");
            Transform nuzzle = transform.FindDeepChild($"FuelNuzzle{i}");
            Transform end = nuzzle != null ? nuzzle.FindDeepChild($"HoseEndPoint") : null;
            Transform exit = nuzzle != null ? nuzzle.FindDeepChild("HoseExitPoint") : null;

            if (anchor == null || end == null)
            {
                Debug.LogWarning($"Не найден точки для шланга {i} в {name}");
                continue;
            }

            // Создаём шланг
            GameObject hose = Instantiate(hosePrefab, anchor.position, Quaternion.identity, transform);

            // Привязываем шланг
            HoseController hc = hose.GetComponent<HoseController>();
            hc.anchorPoint = anchor;
            hc.exitPoint = exit;
            hc.hoseEndPoint = end;
        }
    }
}
