﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedBonus : MonoBehaviour
{
    [Header("Set in Inspector")]
    // 
    public GameObject medPrefab;
    public float speed = 1f;
    public bool spawnBonus = true;
    public float secondsBetweenBonus = 1f;
    //public List<GameObject> medPool;
    Vector2 medPosition;
    void Start()
    {
        spawnBonus = true;
 

        Invoke("SpawnBonus", 2f);
    }
    void SpawnBonus()
    {
        if (spawnBonus)
        {
            GameObject med = Instantiate<GameObject>(medPrefab);
            med.transform.position = new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f)); //позиція яблука рівна позиції яблуні
            Invoke("SpawnBonus", secondsBetweenBonus);  //кожну секунду буде скидатися нове яблуко
        }
    }
    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            medPosition = new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f));
            if (!spawnBonus)
            {
                spawnBonus = true;
            }
        }
        else
            spawnBonus = false;
    }
}
