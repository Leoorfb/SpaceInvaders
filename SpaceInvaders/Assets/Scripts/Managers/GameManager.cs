using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action<float> OnGameSpeedValueChangeEvent;
    public static event Action<int> OnScoreValueChangeEvent;
    public static event Action<bool> OnIsGamePlayingValueChangeEvent;
    public static event Action<int> OnNextLevelEvent;
    
    public int level { get; private set; }

    [SerializeField] private float baseGameSpeed = 1.5f;
    private float GameSpeed = 1.5f;
    [SerializeField] private int Score = 0;

    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject barriers;
    [SerializeField] GameObject player;
    [SerializeField] GameObject aliensSpawner;
    
    private bool IsGamePlaying;

    public bool isGamePlaying
    {
        get
        {
            return IsGamePlaying;
        }
        set
        {
            IsGamePlaying = value;
            OnIsGamePlayingValueChangeEvent?.Invoke(IsGamePlaying);
        }
    }

    public float gameSpeed {
        get
        {
            return GameSpeed;
        }
        set
        {
            GameSpeed = value;
            OnGameSpeedValueChangeEvent?.Invoke(GameSpeed);
        } 
    }

    public int score
    {
        get
        {
            return Score;
        }
        set
        {
            Score = value;
            OnScoreValueChangeEvent?.Invoke(Score);
        }
    }

    private void Start()
    {
        Alien.DeathEvent += LevelEndCheck;
    }

    private void OnDisable()
    {
        Alien.DeathEvent -= LevelEndCheck;
    }

    private void LevelEndCheck(int aliensLeftAlive)
    {
        if (aliensLeftAlive <= 0)
        {
            level++;
            gameSpeed /= 2;
            OnNextLevelEvent?.Invoke(level);
        }
    }

    public void StartGame()
    {
        MainMenuUI.SetActive(false);
        GameUI.SetActive(true);

        player.SetActive(true);

        barriers.SetActive(true);
        Barrier.ActivateBarriers();

        aliensSpawner.SetActive(true);

        score = 0;
        level = 0;
        gameSpeed = baseGameSpeed;

        LevelEndCheck(0);
        isGamePlaying = true;
    }

    public void GameOver()
    {
        GameOverUI.SetActive(true);
        GameEnd();
    }

    public void GameEnd()
    {
        GameUI.SetActive(false);

        player.SetActive(false);

        barriers.SetActive(false);

        aliensSpawner.SetActive(false);
        isGamePlaying = false;
    }
}
