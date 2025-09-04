using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    // ��� �����, ������� ����� ��������� ����� LoadingScene
    public static string NextScene { get; private set; }

    // �������� � ������� Start / Back ������ SceneManager.LoadScene
    public static void Load(string sceneName)
    {
        NextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
}
