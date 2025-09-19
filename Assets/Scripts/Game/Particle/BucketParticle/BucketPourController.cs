using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BucketPourController : MonoBehaviour
{
    [Header("Порог наклона по X")]
    [SerializeField] private float pourAngleThreshold = 60f;

    [Header("Ограничение наклона по X")]
    [SerializeField] private float minAngleX = -111f;
    [SerializeField] private float maxAngleX = 111f;

    [Header("Системы песка")]
    [SerializeField] private ParticleSystem[] sandParticles;

    [Header("Ручка снизу")]
    [SerializeField] private XRGrabInteractable bottomHandle;

    private XRGrabInteractable bucketGrab;
    private bool isHeldByBucketHand;
    private bool isHeldByBottomHand;

    private void Awake()
    {
        bucketGrab = GetComponentInChildren<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        bucketGrab.selectEntered.AddListener(OnBucketGrabbed);
        bucketGrab.selectExited.AddListener(OnBucketReleased);

        bottomHandle.selectEntered.AddListener(OnBottomGrabbed);
        bottomHandle.selectExited.AddListener(OnBottomReleased);
    }

    private void OnDisable()
    {
        bucketGrab.selectEntered.RemoveListener(OnBucketGrabbed);
        bucketGrab.selectExited.RemoveListener(OnBucketReleased);

        bottomHandle.selectEntered.RemoveListener(OnBottomGrabbed);
        bottomHandle.selectExited.RemoveListener(OnBottomReleased);

        StopAllParticles();
    }

    private void OnBucketGrabbed(SelectEnterEventArgs args) => isHeldByBucketHand = true;
    private void OnBucketReleased(SelectExitEventArgs args) => isHeldByBucketHand = false;
    private void OnBottomGrabbed(SelectEnterEventArgs args) => isHeldByBottomHand = true;
    private void OnBottomReleased(SelectExitEventArgs args) => isHeldByBottomHand = false;

    private void Update()
    {
        ClampRotation();

        if (isHeldByBucketHand && isHeldByBottomHand)
        {
            float angleX = transform.localEulerAngles.x;
            angleX = angleX > 180f ? angleX - 360f : angleX;

            bool shouldPour = angleX > pourAngleThreshold || angleX < -pourAngleThreshold;

            if (shouldPour)
                PlayAllParticles();
            else
                StopAllParticles();
        }
        else
        {
            StopAllParticles();
        }
    }

    private void ClampRotation()
    {
        Vector3 angles = transform.localEulerAngles;
        float angleX = angles.x > 180f ? angles.x - 360f : angles.x;
        angleX = Mathf.Clamp(angleX, minAngleX, maxAngleX);
        angles.x = angleX < 0f ? 360f + angleX : angleX;
        transform.localEulerAngles = new Vector3(angles.x, angles.y, angles.z);
    }

    private void PlayAllParticles()
    {
        foreach (var ps in sandParticles)
        {
            if (!ps.isPlaying)
                ps.Play();
        }
    }

    private void StopAllParticles()
    {
        foreach (var ps in sandParticles)
        {
            if (ps.isPlaying)
                ps.Stop();
        }
    }
}
