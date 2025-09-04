using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public float openRotationAngle = 90f; // ���� �������� �����
    public float openSpeed = 2f; // �������� ��������
    private bool isOpening = false;
    public float closeRotationAngle = 180f;
    private bool isClosing = false;
    public float speed = 2f; // �������� ��������

    void Update()
    {
        if (isOpening)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90, 0 ,openRotationAngle ), Time.deltaTime * openSpeed);
        }
        else if (isClosing)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90,180 ,closeRotationAngle ), Time.deltaTime * speed);
        }
    }
    public void ToggleDoor()
    {
        if (!isOpening && !isClosing)
        {
            isOpening = true;
            Invoke(nameof(StartClosing), 3f); // �������� ����� 3 �������
        }
    }

    private void StartClosing()
    {
        isOpening = false;
        isClosing = true;
        Invoke(nameof(ResetState), 2f); // ����� ��������� ����� ��������
    }

    private void ResetState()
    {
        isClosing = false;
    }

}
