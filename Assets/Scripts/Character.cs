using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string gender;
    public string role;
    public string FirstMovementSpriteStr;
    public string LastMovementSpriteStr;
    public int age;
    public int intelligence;
    public int strength;
    private int maxStats = 10;
    private int rand;
    private int typeName;
    //public float GameProgress;
    public float workTime;
    private float workCount = 0.002f;
    public float health;
    public float maxHealth = 100;
    public bool IsInfected;
    public bool IsWork;
    public Vector3 startPosition;
    public Vector3 gatePosition;
    public Vector3 endPosition;
    private SpriteRenderer sprite;
    public Sprite mainSprite;
    public Sprite engineerMaleSprite;
    public Sprite medicMaleSprite;
    public Sprite explorerMaleSprite;
    public Sprite engineerFemaleSprite;
    public Sprite medicFemaleSprite;
    public Sprite explorerFemaleSprite;
    //public Character()
    void Start()
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        IsInfected = false;
        rand = Random.Range(0, 2);
        if (rand == 1)
        {
            gender = "Male";
            typeName = 6;
            name = NVJOBNameGen.Uppercase(NVJOBNameGen.GiveAName(typeName));
        }
        else
        {
            gender = "Female";
            typeName = 5;
            name = NVJOBNameGen.Uppercase(NVJOBNameGen.GiveAName(typeName));
        }
        age = Random.Range(18, 51);
        rand = Random.Range(0, 3);
        if (rand == 0)
        {
            role = "Engineer";
            if (gender == "Male")
            {
                sprite.sprite = engineerMaleSprite;
            }
            else
            {
                sprite.sprite = engineerFemaleSprite;
            }
        }
        else if (rand == 1)
        {
            role = "Medic";
            if (gender == "Male")
            {
                sprite.sprite = medicMaleSprite;
            }
            else
            {
                sprite.sprite = medicFemaleSprite;
            }
        }
        else
        {
            role = "Explorer";
            if (gender == "Male")
            {
                sprite.sprite = explorerMaleSprite;
            }
            else
            {
                sprite.sprite = explorerFemaleSprite;
            }
        }
        for (int i = 0; i < maxStats; i++)
        {
            rand = Random.Range(0, 2);

            if (rand == 0)
            {
                intelligence += 1;
            }
            else
            {
                strength += 1;
            }
        }
        workTime = 0f;
        maxHealth += strength * 5;
        health = maxHealth;
        mainSprite = sprite.sprite;
    }
    //Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            if (health <= 0)
                Destroy(gameObject);
            if (workTime > 0)
            {
                Camera.main.gameObject.GetComponent<Game>().GameProgress += Work();
                workTime -= Time.deltaTime;
            }
        }
    }
    private float Work()
    {
        if(!IsInfected)
        {
            if (role == "Medic")
                return workCount;
            else
                return workCount / 1.5f;
        }
        else 
        {
            if (role == "Medic")
                return workCount/2f;
            else
                return workCount / 3f;
        }
    }
}
