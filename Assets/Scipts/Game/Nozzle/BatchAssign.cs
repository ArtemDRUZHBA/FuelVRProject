using UnityEngine;
using UnityEditor;

public class BatchAssign : MonoBehaviour
{
    public Transform[] sources; // 8 штук
    public HoseController[] targets; // 8 штук

    [ContextMenu("Assign Exit Points")]
    void Assign()
    {
        if (sources.Length != targets.Length)
        {
            Debug.LogError("Количество источников и приёмников не совпадает!");
            return;
        }

        for (int i = 0; i < sources.Length; i++)
        {
            targets[i].exitPoint = sources[i];
            EditorUtility.SetDirty(targets[i]); // чтобы сохранить в сцене
        }

        Debug.Log("Готово! ExitPoint присвоены.");
    }
}
