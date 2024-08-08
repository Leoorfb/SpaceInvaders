using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        GameManager.OnNextLevelEvent += UpdateLevelText;
        GameManager.OnScoreValueChangeEvent += UpdateScoreText;
        PlayerLives.OnLivesValueChangeEvent += UpdateLivesText;

        UpdateScoreText(GameManager.Instance.score);
        UpdateLevelText(GameManager.Instance.level);
        UpdateLivesText(PlayerLives.lives);
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    private void UpdateLevelText(int level)
    {
        levelText.text = level.ToString();
    }

    private void UpdateLivesText(int lives)
    {
        livesText.text = lives.ToString();
    }
}
