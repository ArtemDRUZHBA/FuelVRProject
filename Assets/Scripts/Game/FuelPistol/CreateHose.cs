using UnityEngine;

public class CreateHose : MonoBehaviour
{
    [SerializeField] private GameObject _hosePrefab;
    private int _hoseCount = 4;

    public void InstantiateHose()
    {
        for (int i = 0; i < _hoseCount; i++)
        {
            Transform startPoint = transform.FindDeepChild($"HoseStartPoint{i}");
            Transform fuelPistol = transform.FindDeepChild($"FuelPistol{i}");
            Transform endPoint = fuelPistol.Find("HoseEndPoint");
            Transform additionalPoint = fuelPistol.Find("HoseExitPoint");

            Transform fuelPistolAncor = transform.FindDeepChild($"FuelPistolAnchor{i}");

            if (startPoint == null || endPoint == null)
            {
                Debug.LogWarning($"Не найдены точки для шланга {i} в {name}");
                continue;
            }

            // Создаём шланг
            //GameObject hose = Instantiate(_hosePrefab, fuelPistolAncor, true);
            //hose.transform.localPosition = Vector3.zero;
            //hose.transform.localRotation = Quaternion.identity;

            GameObject hose = Instantiate(_hosePrefab, startPoint.position, Quaternion.identity);
            hose.transform.SetParent(fuelPistolAncor, true);

            hose.name = $"Hose{i}";
            Debug.Log($"Создан шланг {hose}");

            // Привязываем шланг
            HoseController hc = hose.GetComponent<HoseController>();
            hc.anchorPoint = startPoint;
            hc.hoseEndPoint = additionalPoint;
        }
    }
}
