using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string gender;
    public string role;
    public int age;
    public int intelligence;
    public int strength;
    private int maxStats = 10;
    private int rand;
    private int typeName;
    public float health = 100;
    public bool IsInfected;
    //public bool IsPatrol;
    public Vector3 startPosition;
    public Vector3 gatePosition;
    public Vector3 endPosition;
    SpriteRenderer sprite;
    public Sprite mainSprite;
    public string FirstMovementSpriteStr;
    public string LastMovementSpriteStr;
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
        health += strength * 5;
        mainSprite = sprite.sprite;
    }
    //Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}
