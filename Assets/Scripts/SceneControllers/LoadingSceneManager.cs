using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private string _buttonName;
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private Image _progressBarImage;
    [SerializeField] private TMP_Text _loadingText;
    [SerializeField] private GameObject _pressKeyHint;

    public InputFeatureUsage<bool> triggerButton;

    AsyncOperation asyncOperation;

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "LoadScene") StartCoroutine("AsyncLoadScene", PlayerPrefs.GetString("current_scene"));
    }
    
    public void ActivateSceneLoading(string sceneName)
    {
        _buttonName = sceneName;
        PlayerPrefs.SetString("current_scene", _buttonName);
        SceneManager.LoadScene("LoadScene");
    }

    IEnumerator AsyncLoadScene(string sceneName)
    {
        float loadingProgress;
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        _progressBar.SetActive(true);

        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            loadingProgress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            _loadingText.text = $"Загрузка ... {(loadingProgress * 100).ToString("0")}%";
            _progressBarImage.fillAmount = loadingProgress;
            yield return true;
        }
        _progressBar.SetActive(false);
        _pressKeyHint.SetActive(true);
    }

    private void Update()
    {
        if (_pressKeyHint.activeSelf)
            if (Input.anyKeyDown) asyncOperation.allowSceneActivation = true;
    }
}
