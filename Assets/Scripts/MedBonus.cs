using System.Collections;
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
    //Vector2 medPosition;
    void Start()
    {
        spawnBonus = true;

        Invoke("SpawnBonus", 10f);
    }
    void SpawnBonus()
    {
        if (spawnBonus && !PauseMenu.IsPaused)
        {
            GameObject med = Instantiate<GameObject>(medPrefab);
            med.transform.position = new Vector2(Random.Range(-3f, 3f), Random.Range(-0.7f, 0.7f)); 
            Invoke("SpawnBonus", secondsBetweenBonus);  
        }
    }
    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            //medPosition = new Vector2(Random.Range(-3f, 3f), Random.Range(-0.7f, 0.7f));
            if (!spawnBonus)
            {
                spawnBonus = true;
            }
        }
        else
            spawnBonus = false;
    }
}
