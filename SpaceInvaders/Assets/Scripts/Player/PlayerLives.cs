using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : Hittable
{
    public static event Action<int> OnLivesValueChangeEvent;

    public bool isActive = true;
    public int baseLives = 3;
    private static int Lives = 3;
    public static int lives
    {
        get
        {
            return Lives;
        }
        set
        {
            Lives = value;
            OnLivesValueChangeEvent?.Invoke(Lives);
        }
    }

    private void OnEnable()
    {
        lives = baseLives;
    }

    public override void OnHit()
    {
        lives--;
        AudioManager.Instance.Play("HitPlayer");
        if (lives <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

}
