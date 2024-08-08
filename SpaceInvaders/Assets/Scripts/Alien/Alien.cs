using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : Hittable
{
    public static event Action<int> DeathEvent;

    public int scoreReward = 1;
    public float speedReward = .1f;
    public float yLimitToHit = -2.1f;
    [SerializeField] GameObject alienDeathVFX;

    public static int alienCount { get; private set; } 
    private void Start()
    {
        alienCount++;
    }

    private void Update()
    {
        if (transform.position.y < yLimitToHit)
        {
            GameManager.Instance.GameOver();
        }
    }

    public override void OnHit()
    {
        GameManager.Instance.gameSpeed += speedReward;
        GameManager.Instance.score += scoreReward;

        alienCount--;
        DeathEvent?.Invoke(alienCount);
        Instantiate(alienDeathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
