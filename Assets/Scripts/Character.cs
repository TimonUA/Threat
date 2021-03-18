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
    public float workTime;
    private float workCount = 0.007f;
    private float medicDiv;
    private float nomedicDiv;
    private float infectedDiv;
    public float health;
    public float maxHealth = 100f;
    public bool IsInfected;
    public bool IsWork;
    public Vector3 startPosition;
    public Vector3 gatePosition;
    public Vector3 endPosition;
    private Game game;
    private SpriteRenderer sprite;
    public Sprite mainSprite;
    public Sprite engineerMaleSprite;
    public Sprite medicMaleSprite;
    public Sprite explorerMaleSprite;
    public Sprite engineerFemaleSprite;
    public Sprite medicFemaleSprite;
    public Sprite explorerFemaleSprite;
    public Sprite infectedFemaleSprite;
    public Sprite infectedMaleSprite;
    private int st;
    //public Character()
    void Start()
    {
        st = 1;
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
        maxHealth += strength * 5f;
        health = maxHealth;
        mainSprite = sprite.sprite;
    }
    //Update is called once per frame
    void FixedUpdate()
    {
        if (!PauseMenu.IsPaused && !DialogueManager.IsDialogue && !Game.IsEnd)
        {
            if(st==1)
            {
                game = Camera.main.GetComponent<Game>();
                medicDiv = game.medicDiv;
                nomedicDiv = game.nomedicDiv;
                infectedDiv = game.infectedDiv;
                st = 0;
            }
            if (health <= 0)
            {
                if (gameObject == Camera.main.GetComponent<Game>().lastInfoObject)
                    game.CharacterInfo.SetActive(false);
               
                game.crewNumb--;
                //GameObject[] infected = GameObject.FindGameObjectsWithTag("InfectedCollider");
                //GameObject[] crew = GameObject.FindGameObjectsWithTag("Character");
                //if (infected.Equals(gameObject) && infected.Length == 1 && crew.Length > 1)
                //{
                //    game.End(game.WinMenu);
                //    //gameObject.SetActive(false);
                //    Destroy(gameObject);
                //    Destroy(this);
                //}
                //else if (infected.Length == 1 && crew.Length == 1)
                //{
                //    game.End(game.LoseMenu);
                //    //gameObject.SetActive(false);
                //    Destroy(gameObject);
                //}
                //else
                //{
                    //Destroy(gameObject);
                    //Destroy(this);
                //}

                if (gameObject.TryGetComponent<Patrol>(out var patrol) != false && this)
                {
                    Destroy(gameObject.GetComponent<Patrol>());
                    game.IsPatrol = false;
                    game.Patrol();
                }
                if (gameObject.TryGetComponent<MainInfected>(out var mainInfected) != false && this)
                {
                    game.MainInfectedObject = null;
                    Destroy(gameObject.GetComponent<MainInfected>());
                }
                Destroy(gameObject);
                Destroy(this);
            }
            if (workTime > 0)
            {
                game.GameProgress += Work();
                workTime -= Time.deltaTime;
            }
            if(IsInfected)
            {
                if(!gameObject.transform.GetChild(0).TryGetComponent<Infected>(out var infected))
                {
                    gameObject.transform.GetChild(0).gameObject.AddComponent<Infected>();
                }
                //помінять в сложності
                if (game.medBonus > 0.15)
                {
                    game.GameProgress += Work() / (infectedDiv);
                    //Debug.Log("Worrkkkk");
                }
               else
                {
                    if (role == "Medic")
                        game.GameProgress += Work() / (infectedDiv);
                }
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
                return workCount / nomedicDiv;
        }
        else 
        {
            if (role == "Medic")
                return workCount / (medicDiv*1.2f);
            else
                return workCount / (nomedicDiv*1.7f);
        }
    }
}
