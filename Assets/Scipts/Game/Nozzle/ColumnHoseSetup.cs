using UnityEngine;

public class ColumnHoseSetup : MonoBehaviour
{
    public GameObject hosePrefab;      // ������ ������
    public int hoseCount = 8;          // ���������� ����������

    void Start()
    {
        for (int i = 1; i <= hoseCount; i++)
        {
            Transform anchor = transform.FindDeepChild($"HoseStartPoint{i}");

            Transform nuzzle = transform.FindDeepChild($"FuelNuzzle{i}");

            Transform gun = nuzzle != null ? nuzzle.FindDeepChild($"FuelNuzzle{i}") : null;


            if (anchor == null)
            {
                Debug.LogWarning($"�� ������ ����� ��� ������ {i} � {name}");
                continue;
            }

            // ������ �����
            GameObject hose = Instantiate(hosePrefab, anchor.position, Quaternion.identity, transform);

            // ����������� �����
            HoseController hc = hose.GetComponent<HoseController>();
            hc.anchorPoint = anchor;
            hc.gunPoint = gun;
        }
    }
}
