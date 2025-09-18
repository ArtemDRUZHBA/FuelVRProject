using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class NozzleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    private ParticleSystem particleSystem;

    public bool inHand;
    public bool isFueling;
    public float fuelingSpeed;

    private FuelTank fuelTank;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        particleSystem = particle.GetComponent<ParticleSystem>();
        particleSystem.Stop();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out FuelSocket fs) && !inHand)
        {
            rb.isKinematic = true;
            transform.position = fs.transform.position;
            transform.rotation = fs.transform.rotation;
        }
    }

    public void SetFuelTank(FuelTank ft)
    {
        fuelTank = ft;
        if (fuelTank != null && !inHand)
        {
            rb.isKinematic = true;
            transform.position = ft.transform.position;
            transform.rotation = ft.transform.rotation;
            isFueling = true;
        }
    }

    private void FixedUpdate()
    {
        Fueling();
    }

    private void Fueling()
    {
        if (isFueling && fuelTank != null)
        {
            fuelTank.Fueling(fuelingSpeed);
            particleSystem.Stop();
        }
        else if (isFueling && fuelTank == null)
        {
            particleSystem.Play();
        }
        else if (!isFueling)
        {
            particleSystem.Stop();
        }
    }

    private void OnDisable()
    {
        isFueling = false;
        particleSystem.Stop();
    }

    public void NozzleTaked()
    {
        inHand = true;
        isFueling = false;
    }

    public void NozzleThrowned()
    {
        inHand = false;
        isFueling = false;
        rb.isKinematic = false;
    }

    public void StartFueling()
    {
        if (inHand)
            isFueling = true;
    }

    public void StopFueling()
    {
        if (inHand)
            isFueling = false;
    }
}
