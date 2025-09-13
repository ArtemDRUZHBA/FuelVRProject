using UnityEngine;
using UnityEngine.Serialization;

public class HoseController : MonoBehaviour
{
    public Transform anchorPoint; // точка крепления (начало)
    public Transform exitPoint;   // точка выхода из пистолета/колонки
    public Transform hoseEndPoint;    // пистолет (конец)
    public Transform[] bones;     // кости шланга
    public float maxLength = 5f;  // максимальная длина
    public float smooth = 5f;     // плавность движения

    private float boneSpacing;

    void Start()
    {
        if (bones.Length > 1)
            boneSpacing = maxLength / (bones.Length - 1);
    }

    void LateUpdate()
    {
        if (anchorPoint == null || hoseEndPoint == null) return;

        // Ограничение длины
        float dist = Vector3.Distance(anchorPoint.position, hoseEndPoint.position);
        if (dist > maxLength)
        {
            Vector3 dir = (hoseEndPoint.position - anchorPoint.position).normalized;
            Vector3 targetPos = anchorPoint.position + dir * maxLength;
            hoseEndPoint.position = Vector3.Lerp(hoseEndPoint.position, targetPos, Time.deltaTime * 10f);
        }

        // Если exitPoint не задан — fallback на anchorPoint
        Vector3 p0 = anchorPoint.position;
        Vector3 p1 = exitPoint != null ? exitPoint.position : anchorPoint.position;
        Vector3 p2 = hoseEndPoint.position;

        for (int i = 0; i < bones.Length; i++)
        {
            float t = (float)i / (bones.Length - 1);
            Vector3 targetPos;

            // Первая часть: anchor → exitPoint
            if (t < 0.3f)
                targetPos = Vector3.Lerp(p0, p1, t / 0.3f);
            // Вторая часть: exitPoint → gunPoint
            else
                targetPos = Vector3.Lerp(p1, p2, (t - 0.3f) / 0.7f);

            // Лёгкий провис
            float sag = Mathf.Sin(t * Mathf.PI) * 0.2f;
            Vector3 sagOffset = Vector3.down * sag;

            bones[i].position = Vector3.Lerp(bones[i].position, targetPos + sagOffset, Time.deltaTime * smooth);
        }
    }
}
