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
        rb.isKinematic = true;

        if (restPoint == null)
        {
            string number = name.Replace("FuelNuzzle", "");
            Transform pumpRoot = transform.parent; 
            while (pumpRoot != null && !pumpRoot.name.StartsWith("FuelPump"))
            {
                pumpRoot = pumpRoot.parent;

                if (pumpRoot != null)
                    restPoint = pumpRoot.Find($"FuelNuzzleAnchor{number}");

                if (restPoint == null)
                    Debug.LogWarning($"{name}: FuelNuzzleAnchor{number} не найден!");
            }
        }
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
        if (restPoint == null)
        {
            Debug.LogWarning($"{name}: restPoint не найден, возврат невозможен");
            return;
        }

        isHeld = false;
        transform.SetParent(restPoint, true);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        rb.isKinematic = true;
        GetComponent<Collider>().enabled = true;
    }
}
