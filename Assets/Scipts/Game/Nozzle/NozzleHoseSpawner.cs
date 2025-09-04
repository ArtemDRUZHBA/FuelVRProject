using UnityEngine;

public class NozzleHoseSpawner : MonoBehaviour
{
    public GameObject hosePrefab;      // ������ ������
    public GameObject nozzlePrefab;    // ������ ���������
    public int hoseCount = 8;          // ���������� ����������

    void Start()
    {
        for (int i = 1; i <= hoseCount; i++)
        {
            Transform anchor = GameObject.Find($"HoseStartPoint{i}")?.transform;
            if (anchor == null)
            {
                Debug.LogWarning($"�� ������ HoseStartPoint{i}");
                continue;
            }

            // ������ ��������
            GameObject nozzle = Instantiate(nozzlePrefab, anchor.position + Vector3.forward * 1f, Quaternion.identity);

            // ������� ����� ����� ������ � ���������
            Transform hoseEnd = nozzle.transform.Find("HoseEndPoint");
            if (hoseEnd == null)
            {
                Debug.LogWarning($"� ��������� {nozzle.name} ��� HoseEndPoint");
                continue;
            }

            // ������ �����
            GameObject hose = Instantiate(hosePrefab, anchor.position, Quaternion.identity);

            // ����������� �����
            HoseController hc = hose.GetComponent<HoseController>();
            hc.anchorPoint = anchor;
            hc.gunPoint = hoseEnd;
        }
    }
}
