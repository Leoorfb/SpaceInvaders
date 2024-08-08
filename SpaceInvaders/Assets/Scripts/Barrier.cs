using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : Hittable
{
    private static List<GameObject> barriers = new List<GameObject>();

    private void Start()
    {
        barriers.Add(gameObject);
    }

    public override void OnHit()
    {
        gameObject.SetActive(false);
    }

    public static void ActivateBarriers()
    {
        foreach (GameObject barrier in barriers)
        {
            barrier.SetActive(true);
        }
    }
}
