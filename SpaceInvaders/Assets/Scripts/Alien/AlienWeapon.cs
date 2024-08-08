using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienWeapon : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] Transform spawnPosition;

    public static float baseFireCooldown = 5f;
    public static float fireCooldown = 5f;

    public void Fire()
    {
        Bullet newBullet = Instantiate(bulletPrefab, spawnPosition.position, Quaternion.identity);
        newBullet.direction = Vector2.down;
        AudioManager.Instance?.Play("LaserAlien");
    }

    void Start()
    {
        UpdateCooldown(GameManager.Instance.gameSpeed);
        GameManager.OnGameSpeedValueChangeEvent += UpdateCooldown;
    }

    private void OnDisable()
    {
        GameManager.OnGameSpeedValueChangeEvent -= UpdateCooldown;
    }

    private void UpdateCooldown(float gameSpeed)
    {
        fireCooldown = baseFireCooldown / gameSpeed;
    }
}
