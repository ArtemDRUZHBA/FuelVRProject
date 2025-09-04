using UnityEngine;
using UnityEngine.UI;

public class FuelBarController : MonoBehaviour
{
    [Tooltip("Слайдер UI")]
    public Slider slider;

    [HideInInspector] public Transform target;
    [HideInInspector] public Vector3 offset;
    [HideInInspector] public Transform playerCamera;

    void Update()
    {
        if (target == null || playerCamera == null) return;

        // Всегда висим над машиной
        transform.position = target.position + offset;

        // Смотрим на камеру, фиксируя world-up (без «кренов»)
        Vector3 dir = transform.position - playerCamera.position;
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }

    public void SetProgress(float value)
    {
        if (slider == null)
        {
            Debug.LogError("[FuelBarController] slider == null!");
            return;
        }

        slider.value = Mathf.Clamp01(value);
    }
}
