using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Classe que controla a UI do Menu principal.
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject ConfigMenu;
    [SerializeField] Button playButton;
    [SerializeField] Button configButton;
    [SerializeField] Button quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        configButton.onClick.AddListener(OnConfigButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    public void OnPlayButtonClick()
    {
        AudioManager.Instance.Play("MenuConfirm");
        GameManager.Instance.StartGame();
    }

    public void OnConfigButtonClick()
    {
        AudioManager.Instance.Play("MenuConfirm");
        ConfigMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnQuitButtonClick()
    {
        AudioManager.Instance.Play("MenuConfirm");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
