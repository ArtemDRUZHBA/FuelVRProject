using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public Button OpenButton;
    public Button CloseButton;

    public BasePanel PanelManager;

    void Start()
    {
        OpenButton.onClick.AddListener(() => GlobalUIManager.Instance.OpenPanel(PanelManager));
        CloseButton.onClick.AddListener(() => GlobalUIManager.Instance.ClosePanel(PanelManager));
    }
}
