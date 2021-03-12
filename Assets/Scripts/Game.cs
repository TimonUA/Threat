
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
    private GameObject MainInfectedObject;
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
    public float GameProgress;
    public bool biggerAge;
    public bool IsPatrol;
    public int dangerousAge;
    private int st;
    private int rand;
    public int crewNumb;
    //private int lastPatrolCrew;
    private Vector2[] Coordinates = new Vector2[] { new Vector2(5f, 2.6f), new Vector2(4f, -2f), new Vector2(-4f, 3.1f), new Vector2(-4f, -2f) };
    private Vector2[] GateCoordinates = new Vector2[] { new Vector2(3f, 1.6f), new Vector2(2f, -1f), new Vector2(-2f, 2f), new Vector2(-2f, -1f) };
    private Vector2[] EndCoordinates = new Vector2[] { new Vector2(2f, 1f), new Vector2(1f, -0.4f), new Vector2(-1f, 1.6f), new Vector2(-1f, -0.4f) };
    // Start is called before the first frame update
    void Start()
    {
        st = 3;
        IsEnd = false;
        IsPatrol = false;
        //Помінять в сложності
        dangerousAge = 30;
        biggerAge = (Random.value > 0.5f);
        GameProgress = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused && !IsEnd)
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
                        GameProgress += 0.2f;
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
                crew = GameObject.FindGameObjectsWithTag("Character");
                rand = Random.Range(0, crew.Length);
                //lastPatrolCrew = rand;
                crew[rand].AddComponent<Patrol>();
                IsPatrol = true;
            }
        }
        else
        {
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
        else
            Debug.Log("Win");
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
