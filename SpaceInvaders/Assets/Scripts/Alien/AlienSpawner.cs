using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AlienSpawner : Singleton<AlienSpawner>
{
    [SerializeField] private Alien alienPrefab;
    [SerializeField] private Vector2 alienGridSize = new Vector2(7,3);
    private int alienScore = 1;

    private static List<List<Alien>> aliensGrid = new List<List<Alien>>();

    private void OnEnable()
    {
        GameManager.OnNextLevelEvent += StartWave;
        StartCoroutine(AliensWeaponFire());
    }

    private void OnDisable()
    {
        GameManager.OnNextLevelEvent -= StartWave;
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        StopAllCoroutines();
    }

    private void StartWave(int level)
    {
        alienScore += level;
        alienPrefab.scoreReward = alienScore;

        if(level < 6)
        {
            alienGridSize.x += 1;
        }
        else if(level < 8)
        {
            alienGridSize.y += 1;
        }

        SpawnAliens();
    }

    private void SpawnAliens()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.x -= alienGridSize.x/2f;
        aliensGrid.Clear();

        for (int i = 0; i < alienGridSize.x; i++)
        {
            spawnPosition.x += 1;
            spawnPosition.y = transform.position.y;
            aliensGrid.Add(new List<Alien>());
            for(int j = 0; j < alienGridSize.y; j++)
            {
                spawnPosition.y -= .6f;
                Alien alien = Instantiate(alienPrefab, spawnPosition, Quaternion.identity, transform);
                
                aliensGrid[i].Add(alien);
            }
        }
    }

    public static Alien GetRandomBottomAlien()
    {
        Alien alien = null;
        int maxTries = 10;
        int tries = 0;
        while (aliensGrid.Count > 0 && alien == null && tries < maxTries)
        {
            tries++;
            int randomIndex = Random.Range(0, aliensGrid.Count);
            if (aliensGrid[randomIndex].Count > 0)
            {
                for (int i = aliensGrid[randomIndex].Count - 1; i >= 0; i--)
                {
                    alien = aliensGrid[randomIndex][i];
                    if (alien == null)
                        aliensGrid[randomIndex].RemoveAt(i);
                    else
                        break;
                }
                //Debug.Log(randomIndex.ToString() + " - " + (aliensGrid[randomIndex].Count - 1).ToString());
            }
            else
            {
                aliensGrid.RemoveAt(randomIndex);
            }
        }

        return alien;
    }

    public IEnumerator AliensWeaponFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(AlienWeapon.fireCooldown);
            Alien alien = GetRandomBottomAlien();
            if (alien != null)
                alien.GetComponent<AlienWeapon>().Fire();
        }
    }
}
