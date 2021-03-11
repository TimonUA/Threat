
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private GameObject[] crew;
    public GameObject CharacterInfo;
    public GameObject ProgressBar;
    public HealthBar HealthBar;
    public GameObject Collider;
    private GameObject hitObject;
    private GameObject lastObject;
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
    public int dangerousAge;
    private int st;
    private int rand;
    private int lastPatrolCrew;
    // Start is called before the first frame update
    void Start()
    {
        st = 2;
        dangerousAge = 30;
        biggerAge = (Random.value > 0.5f);
        GameProgress = 0f;  
    }
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            if (st == 2)
            {
                crew = GameObject.FindGameObjectsWithTag("Character");
                SetCoordinates(crew);
                CrewCheck(crew);
                st = 1;
            }
            if (st == 1)
            {
                Invoke("Patrol", 2f);
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
                        GameProgress += 0.25f;
                        Destroy(hitObject);
                    }
                    else if (hitObject.tag == "Character")
                    {
                        ShowInfo(hitObject);
                    }
                    else if (hitObject.tag == "Infected")
                    {
                        ShowInfo(hitObject);
                    }
                    else
                    {
                        if (lastObject != null)
                        {
                            lastObject.transform.GetChild(1).gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    CharacterInfo.SetActive(false);
                    if(lastObject != null)
                        lastObject.transform.GetChild(1).gameObject.SetActive(false);
                }
            }

            //GameProgress += 0.001f;
            if (CharacterInfo.activeSelf && lastObject != null)
            {
                HealthBar.SetCurrentHealth(lastObject.GetComponent<Character>().health);
            }
            ProgressBar.GetComponent<FillBar>().CurrentValue = GameProgress;
        }
    }
    public void Patrol()
    { 
        crew = GameObject.FindGameObjectsWithTag("Character");
        rand = Random.Range(0, crew.Length);
        if (rand != lastPatrolCrew)
        {
            lastPatrolCrew = rand;
            crew[rand].AddComponent<Patrol>();
        }
        else
        {
            while(rand==lastPatrolCrew)
                rand = Random.Range(0, crew.Length);
            crew[rand].AddComponent<Patrol>();
        }
    }
    public void ShowInfo(GameObject gameObject)
    {
        if (lastObject != null)
        {
            lastObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        lastObject = gameObject;
        lastObject.transform.GetChild(1).gameObject.SetActive(true);
        CharacterInfo.SetActive(true);
        characterName.text = gameObject.name;
        characterAge.text = $"Age: {gameObject.GetComponent<Character>().age}";
        characterRole.text = gameObject.GetComponent<Character>().role;
        characterGender.text = gameObject.GetComponent<Character>().gender;
        if(gameObject.tag=="Character")
            characterStatus.text = "Healthy";
        else
            characterStatus.text = "Infect";
        HealthBar.SetMaxHealth(gameObject.GetComponent<Character>().maxHealth);
    }
    void SetCoordinates(GameObject[] crew)
    {
        Vector2[] Coordinates= new Vector2[] { new Vector2(5f, 2.6f), new Vector2(4f, -2f), new Vector2(-4f, 3.1f), new Vector2(-4f, -2f) };
        Vector2[] GateCoordinates= new Vector2[] { new Vector2(3f, 1.6f), new Vector2(2f, -1f), new Vector2(-2f, 2f), new Vector2(-2f, -1f) };
        Vector2[] EndCoordinates= new Vector2[] { new Vector2(2f, 1f), new Vector2(1f, -0.4f), new Vector2(-1f, 1.6f), new Vector2(-1f, -0.4f) };
        for (int i=0;i<crew.Length;i++)
        {
            if (i < Coordinates.Length)
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
                if (crew[i].GetComponent<Character>().age > dangerousAge)
                    biggerAge = true;
                else
                    biggerAge = false;
                dangerousGender = crew[i].GetComponent<Character>().gender;
                crew[i].GetComponent<Character>().transform.position = new Vector2(0f, 0.5f);
                crew[i].transform.GetChild(0).gameObject.AddComponent<Infected>();
                //Debug.Log(crew[i]);
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
