using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    // Имя сцены, которую нужно загрузить после LoadingScene
    public static string NextScene { get; private set; }

    // Вызываем в кнопках Start / Back вместо SceneManager.LoadScene
    public static void Load(string sceneName)
    {
        NextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
}
