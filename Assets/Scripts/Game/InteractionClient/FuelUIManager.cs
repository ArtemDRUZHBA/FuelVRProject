using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FuelUIManager : MonoBehaviour
{
    [Tooltip("UI-панель, которая изначально отключена")]
    public GameObject fuelUIPanel;

    [Tooltip("Текстовый компонент, в который выводится фраза")]
    public TextMeshProUGUI fuelUIText;

    // Метод для показа панели с переданной фразой
    public void ShowPhrase(string phrase)
    {
        fuelUIPanel.SetActive(true);
        fuelUIText.text = phrase;
    }

    // Метод для скрытия панели (если потребуется)
    public void HidePanel()
    {
        fuelUIPanel.SetActive(false);
    }
}
