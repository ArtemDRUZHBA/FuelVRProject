using UnityEngine;

public class BucketSandController : MonoBehaviour
{
    [SerializeField] private float pourAngleThreshold = 60f;
    [SerializeField] private float fillRatePerSec = 0.25f;
    [SerializeField] private float emptyRatePerSec = 0.35f;

    [SerializeField] private Transform sandVisual;
    [SerializeField] private float baseLocalY = 0f;
    [SerializeField] private Vector2 xzScaleRange = new Vector2(0.3f, 1f);
    [SerializeField] private Vector2 yOffsetRange = new Vector2(0f, 0.2f);

    [SerializeField] private BucketMouthTrigger mouthTrigger;
    [SerializeField] private ParticleSystem[] pourParticles;

    private float fill01 = 0f;

    private void Start()
    {
        foreach (var ps in pourParticles)
            if (ps != null) ps.Stop();
    }

    private void Update()
    {
        ClampRotation();

        float angleX = GetSignedLocalEulerX();
        bool tiltBeyond = Mathf.Abs(angleX) > pourAngleThreshold;
        bool isFilling = mouthTrigger.IsTouchingSandSource && tiltBeyond;
        bool isPouring = !mouthTrigger.IsTouchingSandSource && tiltBeyond && fill01 > 0f;

        if (isFilling)
            fill01 += fillRatePerSec * Time.deltaTime;
        else if (isPouring)
            fill01 -= emptyRatePerSec * Time.deltaTime;

        fill01 = Mathf.Clamp01(fill01);
        UpdateSandVisual(fill01);
        UpdatePourParticles(isPouring);
    }

    private void ClampRotation()
    {
        Vector3 angles = transform.localEulerAngles;

        float x = (angles.x > 180f) ? angles.x - 360f : angles.x;
        x = Mathf.Clamp(x, -111f, 111f);
        angles.x = (x < 0f) ? 360f + x : x;

        angles.z = 0f;

        transform.localEulerAngles = new Vector3(angles.x, angles.y, angles.z);
    }

    private float GetSignedLocalEulerX()
    {
        float x = transform.localEulerAngles.x;
        return (x > 180f) ? x - 360f : x;
    }

    private void UpdateSandVisual(float t)
    {
        if (sandVisual == null) return;

        bool isEmpty = t <= 0.01f;
        sandVisual.gameObject.SetActive(!isEmpty);
        if (isEmpty) return;

        float xz = Mathf.Lerp(xzScaleRange.x, xzScaleRange.y, t);
        float yOffset = Mathf.Lerp(yOffsetRange.x, yOffsetRange.y, t);

        sandVisual.localScale = new Vector3(xz, 1f, xz);
        sandVisual.localPosition = new Vector3(
            sandVisual.localPosition.x,
            baseLocalY + yOffset,
            sandVisual.localPosition.z
        );
    }

    private void UpdatePourParticles(bool shouldPlay)
    {
        foreach (var ps in pourParticles)
        {
            if (ps == null) continue;
            if (shouldPlay && !ps.isPlaying) ps.Play();
            else if (!shouldPlay && ps.isPlaying) ps.Stop();
        }
    }
}
