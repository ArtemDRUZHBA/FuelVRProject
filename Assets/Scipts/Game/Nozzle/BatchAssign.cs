using UnityEngine;
using UnityEditor;

public class BatchAssign : MonoBehaviour
{
    public Transform[] sources; // 8 ����
    public HoseController[] targets; // 8 ����

    [ContextMenu("Assign Exit Points")]
    void Assign()
    {
        if (sources.Length != targets.Length)
        {
            Debug.LogError("���������� ���������� � ��������� �� ���������!");
            return;
        }

        for (int i = 0; i < sources.Length; i++)
        {
            targets[i].exitPoint = sources[i];
            EditorUtility.SetDirty(targets[i]); // ����� ��������� � �����
        }

        Debug.Log("������! ExitPoint ���������.");
    }
}
