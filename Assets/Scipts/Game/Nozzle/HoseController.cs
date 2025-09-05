using UnityEngine;
using UnityEngine.Serialization;

public class HoseController : MonoBehaviour
{
    public Transform anchorPoint; // точка крепления (начало)
    public Transform gunPoint;    // пистолет (конец)
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
        if (anchorPoint == null || gunPoint == null) return; // защита от null

        // Ограничение длины
        float dist = Vector3.Distance(anchorPoint.position, gunPoint.position);
        if (dist > maxLength)
        {
            Vector3 dir = (gunPoint.position - anchorPoint.position).normalized;
            Vector3 targetPos = anchorPoint.position + dir * maxLength;
            gunPoint.position = Vector3.Lerp(gunPoint.position, targetPos, Time.deltaTime * 10f);
        }

        // Распределяем кости между anchor и gun
        for (int i = 0; i < bones.Length; i++)
        {
            float t = (float)i / (bones.Length - 1);
            Vector3 targetPos = Vector3.Lerp(anchorPoint.position, gunPoint.position, t);

            float sag = Mathf.Sin(t * Mathf.PI) * 0.2f;
            Vector3 sagOffset = Vector3.down * sag; 

            bones[i].position = Vector3.Lerp(bones[i].position, targetPos, Time.deltaTime * smooth);
        }
    }
}
