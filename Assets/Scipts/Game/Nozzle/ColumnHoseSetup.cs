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

            Transform gun = nuzzle != null ? nuzzle.FindDeepChild($"FuelNuzzle{i}") : null;


            if (anchor == null)
            {
                Debug.LogWarning($"Не найден точки для шланга {i} в {name}");
                continue;
            }

            // Создаём шланг
            GameObject hose = Instantiate(hosePrefab, anchor.position, Quaternion.identity, transform);

            // Привязываем шланг
            HoseController hc = hose.GetComponent<HoseController>();
            hc.anchorPoint = anchor;
            hc.gunPoint = gun;
        }
    }
}
