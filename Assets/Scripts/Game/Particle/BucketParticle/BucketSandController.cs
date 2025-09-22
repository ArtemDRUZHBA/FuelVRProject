using UnityEngine;

public class BucketSandController : MonoBehaviour
{
    [Header("Порог наклона")]
    [SerializeField] private float pourAngleThreshold = 60f;

    [Header("Скорости")]
    [SerializeField] private float fillRatePerSec = 0.15f;
    [SerializeField] private float emptyRatePerSec = 0.05f;

    [Header("Визуал песка")]
    [SerializeField] private Transform sandVisual;
    [SerializeField] private float baseLocalY = 0f;
    [SerializeField] private Vector2 xzScaleRange = new Vector2(0.3f, 1.3f);
    [SerializeField] private Vector2 yOffsetRange = new Vector2(0f, 0.65f);

    [Header("Контакты и эффекты")]
    [SerializeField] private BucketMouthTrigger mouthTrigger;
    [SerializeField] private SandIntakeBlocker intakeBlocker;
    [SerializeField] private ParticleSystem[] pourParticles;

    // Текущее заполнение (0..1)
    private float fill01 = 0f;

    private void Start()
    {
        // Гарантируем, что частицы не играют при старте
        if (pourParticles != null)
        {
            foreach (var ps in pourParticles)
                if (ps != null) ps.Stop();
        }

        // Скрываем визуал при пустом состоянии на старте
        UpdateSandVisual(fill01);
    }

    private void Update()
    {

        // Считаем наклон по X и Z (подписанные углы в диапазоне [-180, 180])
        float x = GetSignedAngle(transform.localEulerAngles.x);
        float z = GetSignedAngle(transform.localEulerAngles.z);

        bool tiltBeyond = Mathf.Abs(x) > pourAngleThreshold || Mathf.Abs(z) > pourAngleThreshold;

        // Разделение состояний: набор ИЛИ высыпание
        bool isFilling = tiltBeyond && mouthTrigger != null && mouthTrigger.IsTouchingSandSource;
        bool isPouring = tiltBeyond
            && (mouthTrigger == null || !mouthTrigger.IsTouchingSandSource)
            && fill01 > 0f
            && (intakeBlocker == null || !intakeBlocker.IsInsideIntakeZone);

        if (isFilling)
            fill01 += fillRatePerSec * Time.deltaTime;
        else if (isPouring)
            fill01 -= emptyRatePerSec * Time.deltaTime;

        fill01 = Mathf.Clamp01(fill01);

        UpdateSandVisual(fill01);
        UpdatePourParticles(isPouring && fill01 > 0f);
    }

    private float GetSignedAngle(float euler)
    {
        return (euler > 180f) ? euler - 360f : euler;
    }

    private void UpdateSandVisual(float t)
    {
        if (sandVisual == null) return;

        bool isEmpty = t <= 0.01f;
        if (sandVisual.gameObject.activeSelf == isEmpty)
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
        if (pourParticles == null) return;

        foreach (var ps in pourParticles)
        {
            if (ps == null) continue;

            if (shouldPlay)
            {
                if (!ps.isPlaying) ps.Play();
            }
            else
            {
                if (ps.isPlaying) ps.Stop();
            }
        }
    }
}
