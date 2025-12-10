using UnityEngine;

public class HoseController : MonoBehaviour
{
    public Transform[] bones;
    public Transform anchorPoint;
    public Transform hoseEndPoint;

    private Rigidbody firstRb;
    private Rigidbody lastRb;
    private Vector3 lastEndPosition;

    void Start()
    {
        if (bones.Length > 0)
        {
            firstRb = bones[0].GetComponent<Rigidbody>();
            lastRb = bones[bones.Length - 1].GetComponent<Rigidbody>();

            // фиксируем первую кость
            firstRb.isKinematic = true;
            bones[0].position = anchorPoint.position;
            lastEndPosition = hoseEndPoint.position;

            // расставляем остальные кости, начиная со второй
            for (int i = 1; i < bones.Length; i++)
            {
                float t = (float)i / (bones.Length - 1);
                bones[i].position = Vector3.Lerp(anchorPoint.position, hoseEndPoint.position, t);
            }
        }
    }


    void FixedUpdate()
    {
        if (lastRb != null && hoseEndPoint != null)
        {
            Vector3 current = hoseEndPoint.position;

            // Двигаем только если позиция изменилась
            if ((current - lastEndPosition).sqrMagnitude > 0.0001f)
            {
                lastRb.MovePosition(current);
                lastEndPosition = current;
            }
            else
            {
                // если движения нет — усыпляем Rigidbody для оптимизации
                foreach (var bone in bones)
                {
                    Rigidbody rb = bone.GetComponent<Rigidbody>();
                    if (rb != null && !rb.isKinematic)
                        rb.Sleep();
                }
            }
        }
    }
}
