using UnityEngine;

public class BucketBase : MonoBehaviour
{
    [Header("Ограничение наклона по X")]
    [SerializeField] protected float minAngleX = -111f;
    [SerializeField] protected float maxAngleX = 111f;

    [Header("Порог наклона по X")]
    [SerializeField] protected float pourAngleThreshold = 60f;

    protected float GetSignedLocalEulerX()
    {
        float x = transform.localEulerAngles.x;
        return (x > 180f) ? x - 360f : x;
    }

    protected void ClampRotationX()
    {
        Vector3 angles = transform.localEulerAngles;
        float x = GetSignedLocalEulerX();
        x = Mathf.Clamp(x, minAngleX, maxAngleX);
        angles.x = (x < 0f) ? 360f + x : x;
        transform.localEulerAngles = new Vector3(angles.x, angles.y, angles.z);
    }

    protected bool IsTiltBeyondThreshold()
    {
        float angleX = GetSignedLocalEulerX();
        return Mathf.Abs(angleX) > pourAngleThreshold;
    }
}
