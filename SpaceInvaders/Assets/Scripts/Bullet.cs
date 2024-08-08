using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Hittable
{
    public event Action<bool> HitEvent;
    [SerializeField] private float baseSpeed = .05f;
    private float speed = .05f;

    [SerializeField] private LayerMask BarrierLayer;

    public Vector2 direction = Vector2.up;

    private void OnEnable()
    {
        speed = GameManager.Instance.gameSpeed * baseSpeed;
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Hittable>(out Hittable hittable))
        {
            HitEvent?.Invoke(true);
            hittable.OnHit();
        }
        else
        {
            HitEvent?.Invoke(false);
        }

        Destroy(gameObject);
    }

    public override void OnHit()
    {
        Destroy(gameObject);
    }
}
