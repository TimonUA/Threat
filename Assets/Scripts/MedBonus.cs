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
    void Start()
    {
        spawnBonus = true;

        Invoke("SpawnBonus", 10f);
    }
    void SpawnBonus()
    {
        if (spawnBonus && !PauseMenu.IsPaused && !Game.IsEnd && !DialogueManager.IsDialogue && Camera.main.GetComponent<Game>().DialogueCheck() <= 2)
        {
            GameObject med = Instantiate<GameObject>(medPrefab);
            med.transform.position = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-0.5f, 0.5f)); 
            Invoke("SpawnBonus", secondsBetweenBonus);  
        }
    }
    void Update()
    {
        if (!PauseMenu.IsPaused && !Game.IsEnd && !DialogueManager.IsDialogue)
        {
            if (!spawnBonus)
            {
                spawnBonus = true;
                SpawnBonus();
            }
        }
        else
            spawnBonus = false;
    }
}
