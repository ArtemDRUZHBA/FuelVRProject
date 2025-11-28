using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class FuelPistolTrigger : MonoBehaviour
{
    [Header("XR Input Action (триггер контроллера)")]
    [SerializeField] private InputActionReference triggerAction;

    [SerializeField] public new ParticleSystem particleSystem;


    public float fuelingSpeed;

    private FuelTank fuelTank;
    private Rigidbody rb;
    private XRGrabInteractable grab;
    private IXRSelectInteractor interactor;
    private CreateFuelPistol _fuelPistolAnchors;

    public bool inHand;
    public bool isFueling;
    private bool triggerPressedLastFrame;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        particleSystem.GetComponent<ParticleSystem>();
        grab = GetComponent<XRGrabInteractable>();

        // Подписываемся на события XRGrabInteractable
        grab.activated.AddListener(OnActivated);
        grab.deactivated.AddListener(OnDeactivated);
    }
    private void Update()
    {
        // Проверяем кнопку на VR джойстике
        if (inHand && triggerAction != null)
        {
            bool pressed = triggerAction.action.IsPressed();

            if (pressed && !isFueling)
                StartFueling();
            else if (!pressed && isFueling)
                StopFueling();
        }
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
        if (fuelTank == null)
        {
            isFueling = false;
        }
        else if (fuelTank != null && !inHand)
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
            fuelTank.FuelInTank(fuelingSpeed);
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

    public void OnDisable()
    {
        isFueling = false;
        particleSystem.Stop();
    }

    public void FuelPistolTaked()
    {
        inHand = true;
        isFueling = false;
    }

    public void FuelPistolThrowned()
    {
        inHand = false;
        isFueling = false;
        rb.isKinematic = false;
    }

    public void StartFueling()
    {
            isFueling = true;
    }

    public void StopFueling()
    {
            isFueling = false;
    }
    private void OnDestroy()
    {
        // Отписка, чтобы не было утечек
        grab.activated.RemoveListener(OnActivated);
        grab.deactivated.RemoveListener(OnDeactivated);
    }

    private void OnActivated(ActivateEventArgs args)
    {
        if (inHand) StartFueling();
    }

    private void OnDeactivated(DeactivateEventArgs args)
    {
        if (inHand) StopFueling();
    }
}
