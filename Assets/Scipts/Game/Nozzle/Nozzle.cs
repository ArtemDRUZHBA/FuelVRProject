using UnityEngine;

[RequireComponent(typeof(MeshCollider), typeof(Rigidbody))]
public class Nozzle : MonoBehaviour, INozzle
{
    [SerializeField] public Transform restPoint;
    [SerializeField] private Rigidbody rb;

    private bool isHeld = false;
    private Transform holder;

    public bool IsHeld => isHeld;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        ReturnToRest();
    }

    public void Interact()
    {
        if (isHeld) ReturnToRest();
        else AttachTo(holder); // holder должен быть задан из Interactor
    }

    public void AttachTo(Transform targetHand)
    {
        holder = targetHand;
        isHeld = true;
        transform.SetParent(holder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }

    public void ReturnToRest()
    {
        isHeld = false;
        transform.SetParent(restPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        rb.isKinematic = true;
        GetComponent<Collider>().enabled = true;
    }
}
