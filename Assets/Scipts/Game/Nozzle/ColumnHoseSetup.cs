using UnityEngine;

public class ColumnHoseSetup : MonoBehaviour
{
    public GameObject hosePrefab;
    public int hoseCount = 8;

    void Start()
    {
        for (int i = 1; i <= hoseCount; i++)
        {
            Transform anchor = transform.FindDeepChild($"HoseStartPoint{i}");
            Transform nozzleAnchor = transform.FindDeepChild($"FuelNozzleAnchor{i}");
            Transform end = nozzleAnchor != null ? nozzleAnchor.FindDeepChild("HoseEndPoint") : null;
            Transform exit = nozzleAnchor != null ? nozzleAnchor.FindDeepChild("HoseExitPoint") : null;

            if (anchor == null || end == null)
            {
                Debug.LogWarning($"�� ������� ����� ��� ������ {i} � {name}");
                continue;
            }

            // ������ �����
            GameObject hose = Instantiate(hosePrefab, anchor.position, Quaternion.identity, transform);
            hose.name = $"Hose{i}";

            // ����������� �����
            HoseController hc = hose.GetComponent<HoseController>();
            hc.anchorPoint = anchor;
            hc.exitPoint = exit;
            hc.hoseEndPoint = end;
        }
    }
}
