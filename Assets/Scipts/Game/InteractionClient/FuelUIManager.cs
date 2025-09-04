using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FuelUIManager : MonoBehaviour
{
    [Tooltip("UI-������, ������� ���������� ���������")]
    public GameObject fuelUIPanel;

    [Tooltip("��������� ���������, � ������� ��������� �����")]
    public TextMeshProUGUI fuelUIText;

    // ����� ��� ������ ������ � ���������� ������
    public void ShowPhrase(string phrase)
    {
        fuelUIPanel.SetActive(true);
        fuelUIText.text = phrase;
    }

    // ����� ��� ������� ������ (���� �����������)
    public void HidePanel()
    {
        fuelUIPanel.SetActive(false);
    }
}
