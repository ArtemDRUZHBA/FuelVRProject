using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BucketPourController : MonoBehaviour
{
    [Header("Порог наклона")]
    [SerializeField] private float pourAngleThreshold = 60f;

    [Header("Particle System")]
    [SerializeField] private ParticleSystem sandParticles;

    [Header("Bottom Handle")]
    [SerializeField] private XRGrabInteractable bottomHandle;

    private XRGrabInteractable bucketGrab;
    private bool isHeldByBucketHand;
    private bool isHeldByBottomHand;

    private void Awake()
    {
        bucketGrab = GetComponent<XRGrabInteractable>();
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
    }

    private void OnBucketGrabbed(SelectEnterEventArgs args) => isHeldByBucketHand = true;
    private void OnBucketReleased(SelectExitEventArgs args) => isHeldByBucketHand = false;
    private void OnBottomGrabbed(SelectEnterEventArgs args) => isHeldByBottomHand = true;
    private void OnBottomReleased(SelectExitEventArgs args) => isHeldByBottomHand = false;

    private void Update()
    {
        if (isHeldByBucketHand && isHeldByBottomHand)
        {
            float angleX = Mathf.Abs(transform.localEulerAngles.x);
            float angleZ = Mathf.Abs(transform.localEulerAngles.z);

            // Учитываем переход через 360°
            angleX = angleX > 180 ? 360 - angleX : angleX;
            angleZ = angleZ > 180 ? 360 - angleZ : angleZ;

            bool shouldPour = angleX > pourAngleThreshold || angleZ > pourAngleThreshold;

            if (shouldPour && !sandParticles.isPlaying)
                sandParticles.Play();
            else if (!shouldPour && sandParticles.isPlaying)
                sandParticles.Stop();
        }
        else
        {
            sandParticles.Stop();
        }
    }
}
