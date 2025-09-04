using UnityEngine;
using UnityEngine.UI;

public class ExitButtonSetup : MonoBehaviour
{
    public GameObject exitPanel; // Ссылка на окно выхода
    public Button exitButton; // Ссылка на кнопку "Выход"

    void Start()
    {
        if (exitButton != null) // Проверяем, что кнопка существует
        {
            exitButton.onClick.AddListener(OpenExitPanel);
        }
    }

    void OpenExitPanel()
    {
        exitPanel.SetActive(true); // Показываем окно выхода
    }
}
