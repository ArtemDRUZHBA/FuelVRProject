using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private HoseLeakSpawner hoseLeakSpawner;
    [SerializeField] private NozzleTrigger[] nozzles;
    private Animator animator;
    private bool isEnabled = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isEnabled", isEnabled);
    }

    private void OnTriggerEnter(Collider other)
    {

        isEnabled = !isEnabled;
        animator.SetBool("isEnabled", isEnabled);

        foreach (NozzleTrigger nozzle in nozzles)
            nozzle.enabled = isEnabled;

        if (hoseLeakSpawner != null)
            hoseLeakSpawner.StopLeak();
    }
}
