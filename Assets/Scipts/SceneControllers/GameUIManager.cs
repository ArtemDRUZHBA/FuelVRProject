using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public BasePanel pausePanelManager;
    public BasePanel settingsPanelManager;

    public Button resumeButton;
    public Button settingsButton;

    void Start()
    {
        resumeButton.onClick.AddListener(() => GlobalUIManager.Instance.ClosePanel(pausePanelManager));
        settingsButton.onClick.AddListener(() => GlobalUIManager.Instance.OpenPanel(settingsPanelManager));
    }
}
