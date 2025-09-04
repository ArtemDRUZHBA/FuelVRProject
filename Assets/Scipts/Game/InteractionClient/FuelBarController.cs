using UnityEngine;
using UnityEngine.UI;

public class FuelBarController : MonoBehaviour
{
    [Tooltip("������� UI")]
    public Slider slider;

    [HideInInspector] public Transform target;
    [HideInInspector] public Vector3 offset;
    [HideInInspector] public Transform playerCamera;

    void Update()
    {
        if (target == null || playerCamera == null) return;

        // ������ ����� ��� �������
        transform.position = target.position + offset;

        // ������� �� ������, �������� world-up (��� �������)
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
