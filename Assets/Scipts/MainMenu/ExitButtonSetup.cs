using UnityEngine;
using UnityEngine.UI;

public class ExitButtonSetup : MonoBehaviour
{
    public GameObject exitPanel; // ������ �� ���� ������
    public Button exitButton; // ������ �� ������ "�����"

    void Start()
    {
        if (exitButton != null) // ���������, ��� ������ ����������
        {
            exitButton.onClick.AddListener(OpenExitPanel);
        }
    }

    void OpenExitPanel()
    {
        exitPanel.SetActive(true); // ���������� ���� ������
    }
}
