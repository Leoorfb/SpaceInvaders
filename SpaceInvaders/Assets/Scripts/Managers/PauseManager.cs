using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe controla o pause do jogo.
/// </summary>
public class PauseManager : Singleton<PauseManager>
{

    public bool isGamePaused = false;
    public GameObject pauseScreen;

    private void Start()
    {
        UnpauseGame();
    }


    public void PauseGame()
    {
        //Debug.Log("GAME PAUSE");

        isGamePaused = true;
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        isGamePaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }


    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!isGamePaused)
            {
                pauseScreen.SetActive(true);
                PauseGame();
            }
            else
                UnpauseGame();
        }
    }
}
