using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
