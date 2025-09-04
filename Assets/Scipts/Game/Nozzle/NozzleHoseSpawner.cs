using UnityEngine;

public class NozzleHoseSpawner : MonoBehaviour
{
    public GameObject hosePrefab;      // Префаб шланга
    public GameObject nozzlePrefab;    // Префаб пистолета
    public int hoseCount = 8;          // Количество пистолетов

    void Start()
    {
        for (int i = 1; i <= hoseCount; i++)
        {
            Transform anchor = GameObject.Find($"HoseStartPoint{i}")?.transform;
            if (anchor == null)
            {
                Debug.LogWarning($"Не найден HoseStartPoint{i}");
                continue;
            }

            // Создаём пистолет
            GameObject nozzle = Instantiate(nozzlePrefab, anchor.position + Vector3.forward * 1f, Quaternion.identity);

            // Находим точку конца шланга в пистолете
            Transform hoseEnd = nozzle.transform.Find("HoseEndPoint");
            if (hoseEnd == null)
            {
                Debug.LogWarning($"В пистолете {nozzle.name} нет HoseEndPoint");
                continue;
            }

            // Создаём шланг
            GameObject hose = Instantiate(hosePrefab, anchor.position, Quaternion.identity);

            // Привязываем шланг
            HoseController hc = hose.GetComponent<HoseController>();
            hc.anchorPoint = anchor;
            hc.gunPoint = hoseEnd;
        }
    }
}
