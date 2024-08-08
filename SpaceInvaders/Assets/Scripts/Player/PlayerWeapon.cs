using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] InputReader inputReader;
    [SerializeField] Transform spawnPosition;

    private bool canFire = true;

    private void OnEnable()
    {
        inputReader.FireEvent += Fire;
        canFire = true;

    }
    private void OnDisable()
    {
        inputReader.FireEvent -= Fire;
    }

    private void Fire(bool shouldFire)
    {
        if (shouldFire && canFire)
        {
            Bullet newBullet = Instantiate(bulletPrefab, spawnPosition.position, Quaternion.identity);
            newBullet.HitEvent += AllowFire;
            canFire = false;
            AudioManager.Instance?.Play("LaserPlayer");
        }
    }

    private void AllowFire(bool hitAllien)
    {
        canFire = true;
    }


}
