using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private Button backToMenuButton;

    private void Awake()
    {
        if (backToMenuButton == null)
            Debug.LogError("GameUIController: не назначена кнопка backToMenuButton!");
    }

    private void Start()
    {
        backToMenuButton.onClick.RemoveAllListeners();
        backToMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.Load("MenuScene");
        });
    }
}
