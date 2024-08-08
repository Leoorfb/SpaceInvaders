using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private GameObject mainMenu;

    private void Start()
    {
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
    }

    private void OnEnable()
    {
        finalScore.text = GameManager.Instance.score.ToString();
    }

    public void OnMainMenuButtonClick()
    {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
        AudioManager.Instance.Play("MenuConfirm");
    }
}
