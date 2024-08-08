using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeedX = 1f;
    private static float speedX = 1f;
    [SerializeField] float speedY = -.5f;

    [SerializeField] private LayerMask screenLimitLayer;

    public static Vector3 direction = Vector3.right;
    private static bool changingDirection = false;
    [SerializeField] private float cooldown = .2f;


    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        UpdateSpeed(GameManager.Instance.gameSpeed);
        GameManager.OnGameSpeedValueChangeEvent += UpdateSpeed;
    }

    private void OnDisable()
    {
        GameManager.OnGameSpeedValueChangeEvent -= UpdateSpeed;
        ResetChangeDirectionCooldown();
    }

    private void FixedUpdate()
    {
        _rb.velocity = direction * speedX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((screenLimitLayer & (1 << collision.gameObject.layer)) != 0)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        if (changingDirection) 
            return;
        
        changingDirection = true;
        direction.x = -direction.x;
        direction.y = speedY;

        StartCoroutine(ChangeDirectionCooldown());
    }

    private IEnumerator ChangeDirectionCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        ResetChangeDirectionCooldown();
    }

    private void ResetChangeDirectionCooldown()
    {
        changingDirection = false;
        direction.y = 0;
    }

    private void UpdateSpeed(float gameSpeed)
    {
        speedX = baseSpeedX * gameSpeed;
    }
}
