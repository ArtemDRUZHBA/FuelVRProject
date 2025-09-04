using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private string sceneName = "GameScene";

    void Start()
    {
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(LoadGameScene);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
