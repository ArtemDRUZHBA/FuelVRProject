using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRParticleToggle : MonoBehaviour
{
    [Header("Particle System для управления")]
    [SerializeField] private ParticleSystem targetSystem;

    [Header("XR Input Action")]
    [SerializeField] private InputActionReference toggleAction;

    private bool isActive = false;

    private void OnEnable()
    {
        if (toggleAction != null)
            toggleAction.action.performed += OnToggle;
    }

    private void OnDisable()
    {
        if (toggleAction != null)
            toggleAction.action.performed -= OnToggle;
    }

    private void OnToggle(InputAction.CallbackContext ctx)
    {
        isActive = !isActive;

        if (isActive)
            targetSystem.Play();
        else
            targetSystem.Stop();
    }
}
