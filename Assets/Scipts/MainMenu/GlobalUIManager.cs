using UnityEngine;

public class GlobalUIManager : MonoBehaviour
{
    public static GlobalUIManager Instance;

    [SerializeField] private BasePanel exitPanel;
    [SerializeField] private BasePanel settingsPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // UI менеджер остаётся при смене сцен
        }
        else
        {
            Destroy(gameObject); // Удаляем дубликаты при смене сцен
        }
    }

    public void OpenPanel(BasePanel panel)
    {
        panel.OpenPanel();
    }

    public void ClosePanel(BasePanel panel)
    {
        panel.ClosePanel();
    }
}
