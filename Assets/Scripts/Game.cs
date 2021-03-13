
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static bool IsEnd;
    private GameObject[] crew;
    public GameObject CharacterInfo;
    public GameObject MainInfectedObject;
    public GameObject ProgressBar;
    public GameObject WinMenu;
    public GameObject LoseMenu;
    public HealthBar HealthBar;
    //public GameObject Collider;
    private GameObject hitObject;
    public GameObject lastInfoObject;
    public string[] FirstMovementStr;
    public string[] LastMovementStr;
    public string dangerousGender;
    public Text characterName;
    public Text characterAge;
    public Text characterRole;
    public Text characterGender;
    public Text characterStatus;
    public Tilemap tilemap;
    public TileBase tile;
    public Dialogue InfectedDialogue;
    public Dialogue GameDialogue;
    public float GameProgress;
    public float medBonus;
    public float medicDiv;
    public float nomedicDiv;
    public float infectedDiv;
    public bool biggerAge;
    public bool IsPatrol;
    public int dangerousAge;
    private int st;
    private int dt;
    private int rand;
    public int crewNumb;
    //private int lastPatrolCrew;
    private Vector2[] Coordinates = new Vector2[] { new Vector2(5f, 2.6f), new Vector2(4f, -2f), new Vector2(-4f, 3.1f), new Vector2(-4f, -2f) };
    private Vector2[] GateCoordinates = new Vector2[] { new Vector2(3f, 1.6f), new Vector2(2f, -1f), new Vector2(-2f, 2f), new Vector2(-2f, -1f) };
    private Vector2[] EndCoordinates = new Vector2[] { new Vector2(2f, 1f), new Vector2(1f, -0.4f), new Vector2(-1f, 1.6f), new Vector2(-1f, -0.4f) };
    // Start is called before the first frame update
    void Start()
    {
        dt = 3;
        st = 3;
        IsEnd = false;
        IsPatrol = false;
        GameDialogue = new Dialogue();
        GameDialogue.name = "Base AI";
        //Помінять в сложності
        dangerousAge = 30;
        biggerAge = (Random.value > 0.5f);
        GameProgress = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused && !IsEnd && !DialogueManager.IsDialogue)
        {          
            
            if (st == 3)
            {
                crew = GameObject.FindGameObjectsWithTag("Character");
                crewNumb = crew.Length;
                SetCoordinates(crew);
                CrewCheck(crew);
                st = 2;
            }
            else if(st == 2)
            {
                Invoke("Patrol", 2f);
                st = 0;
            }
            else if (st == 1)
            {
                Patrol();
                st = 0;
            }
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
               
                if (hit.collider != null)
                {
                    hitObject = hit.collider.gameObject;
                    if (hitObject.tag == "Med")
                    {
                        GameProgress += medBonus*1f;
                        Destroy(hitObject);
                    }
                    else if (hitObject.tag == "Character")
                    {
                        ShowInfo(hitObject);
                    }
                    else if (hitObject.tag == "Infected" || hitObject.tag == "MainInfected")
                    {
                        ShowInfo(hitObject);
                    }
                    else
                    {
                        if (lastInfoObject != null)
                        {
                            lastInfoObject.transform.GetChild(1).gameObject.SetActive(false);
                            CharacterInfo.SetActive(false);
                        }
                    }
                }
                else
                {
                    
                    if(lastInfoObject != null)
                    {
                        CharacterInfo.SetActive(false);
                        lastInfoObject.transform.GetChild(1).gameObject.SetActive(false);
                    }
                        
                }
            }
            if (CharacterInfo.activeSelf)
            {

                if (lastInfoObject != null)
                {
                    HealthBar.SetCurrentHealth(lastInfoObject.GetComponent<Character>().health);
                    if (lastInfoObject.transform.GetChild(0).tag == "CharacterCollider")
                        characterStatus.text = "Healthy";
                    else
                        characterStatus.text = "Infect";
                }
            }
            if (MainInfectedObject == null)
                st = 1;
            ProgressBar.GetComponent<FillBar>().CurrentValue = GameProgress;
            if (GameProgress >= 100 )
            {
                End(WinMenu);
            }
            if (crewNumb == 0)
            {
                End(LoseMenu);
            }
        }
    }
    public void End(GameObject Menu)
    {
        Menu.SetActive(true);
        IsEnd = true;
    }
    public void Patrol()
    {
        if (MainInfectedObject != null)
        {
            if (!IsPatrol)
            {
                if(dt==3)
                {
                    if(medBonus==0.015)
                        GameDialogue.sentences = new string[] { "Stay near infected dangerously, crew member has chance to infect, but this only way for crew member to develop a vaccine", "So choose who go to infect reasonably", "The team itself directs 1 crew member to the infect", "But, you can choose who go to infect, just touch to gate,and gate cloose", "This means crew member can't go to infect", "Also base hospital develop vaccine independently, just touch vaccine symbols for develop vaccine" };
                    else
                        GameDialogue.sentences = new string[] { "Stay near infected dangerously, crew member has chance to infect, but this only way for crew member to develop a vaccine","Also crew can develop vaccine even if they infected, but much slower","So choose who go to infect reasonably","The team itself directs 1 crew member to the infect","But, you can choose who go to infect, just touch to gate,and gate cloose","This means crew member can't go to infect","Also base hospital develop vaccine independently, just touch vaccine symbols for develop vaccine"};
                    gameObject.GetComponent<DialogueTrigger>().dialogue = GameDialogue;
                    gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                    dt = 2;
                }
                crew = GameObject.FindGameObjectsWithTag("Character");
                rand = Random.Range(0, crew.Length);
                //lastPatrolCrew = rand;
                crew[rand].AddComponent<Patrol>();
                IsPatrol = true;
            }
        }
        else
        {
            if (dt == 1)
            {
                GameDialogue.sentences = new string[] { "After infected death, other infected go to hospital, if all infected die, and survive at least 1 crew member you win","Or if you do vaccine, and survive anyone you win"};
                gameObject.GetComponent<DialogueTrigger>().dialogue = GameDialogue;
                gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                dt = 0;
            }
            RePosition();
            Patrol();
        }
    }
    public void RePosition()
    {
        crew = GameObject.FindGameObjectsWithTag("InfectedCollider");
        if (crew.Length > 0)
        {
            rand = Random.Range(0, crew.Length);
            MainInfectedObject = crew[rand].transform.parent.gameObject;
            crew[rand].transform.parent.gameObject.GetComponent<Character>().IsInfected = true;
            crew[rand].transform.parent.gameObject.tag = "MainInfected";
            if (crew[rand].transform.parent.gameObject.TryGetComponent<Patrol>(out var patrol))
            {
                Destroy(crew[rand].transform.parent.gameObject.GetComponent<Patrol>());
                IsPatrol = false;
            }
            crew[rand].transform.parent.gameObject.AddComponent<MainInfected>();
        }
    }
    public int DialogueCheck()
    {
        return dt;
    }
    public void AirVentDialogue()
    {
        if(dt==2)
        {
            GameDialogue.sentences = new string[] { "It looks like infect is spreading now through air vent","It's means all crew under threat, but all crew develop a vaccine" };
            gameObject.GetComponent<DialogueTrigger>().dialogue = GameDialogue;
            gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            dt = 1;
        }
    }
    public void ShowInfo(GameObject gameObject)
    {
        if (lastInfoObject != null)
        {
            lastInfoObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        lastInfoObject = gameObject;
        lastInfoObject.transform.GetChild(1).gameObject.SetActive(true);
        CharacterInfo.SetActive(true);
        characterName.text = gameObject.name;
        characterAge.text = $"Age: {gameObject.GetComponent<Character>().age}";
        characterRole.text = gameObject.GetComponent<Character>().role;
        characterGender.text = gameObject.GetComponent<Character>().gender;
        if(gameObject.transform.GetChild(0).tag=="CharacterCollider")
            characterStatus.text = "Healthy";
        else
            characterStatus.text = "Infect";
        HealthBar.SetMaxHealth(gameObject.GetComponent<Character>().maxHealth);
    } 
    void SetCoordinates(GameObject[] crew)
    {
        for (int i=0;i<crew.Length;i++)
        {
            if (i < crew.Length-1)
            {
                crew[i].GetComponent<Character>().transform.position = Coordinates[i];
                crew[i].GetComponent<Character>().startPosition = crew[i].GetComponent<Character>().transform.position;
                crew[i].GetComponent<Character>().gatePosition = GateCoordinates[i];
                crew[i].GetComponent<Character>().endPosition = EndCoordinates[i];
                crew[i].GetComponent<Character>().FirstMovementSpriteStr = FirstMovementStr[i];
                crew[i].GetComponent<Character>().LastMovementSpriteStr = LastMovementStr[i];
            }
            else
            {
                MainInfectedObject = crew[i];
                if (crew[i].GetComponent<Character>().age > dangerousAge)
                    biggerAge = true;
                else
                    biggerAge = false;
                dangerousGender = crew[i].GetComponent<Character>().gender;
                crew[i].GetComponent<Character>().transform.position = new Vector2(0f, 0.5f);
                crew[i].transform.GetChild(0).gameObject.AddComponent<Infected>();
                crew[i].tag = "MainInfected";
                crew[i].GetComponent<SpriteRenderer>().sprite=crew[i].GetComponent<Character>().mainSprite;
                crew[i].AddComponent<DialogueTrigger>();
                InfectedDialogue.name = crew[i].name;
                InfectedDialogue.sentences = new string[] { "Oops, it seems I feel bad, I'm going to the hospital" };
                crew[i].GetComponent<DialogueTrigger>().dialogue = InfectedDialogue;
                crew[i].GetComponent<DialogueTrigger>().TriggerDialogue();
            }

        }
    }
    void CrewCheck(GameObject[] crew)
    {
        bool IsMedic = false;
        for(int i=0;i<crew.Length;i++)
        {
            if (crew[i].GetComponent<Character>().role == "Medic")
                IsMedic = true;
        }
        if(!IsMedic)
        {
            rand = Random.Range(0, crew.Length);
            crew[rand].GetComponent<Character>().role = "Medic";
        }

    }
}
