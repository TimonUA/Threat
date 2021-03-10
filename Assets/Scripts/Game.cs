
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Game : MonoBehaviour
{
    private GameObject[] crew;
    public string[] FirstMovementStr;
    public string[] LastMovementStr;
    public Tilemap tilemap;
    public TileBase tile;
    public float GameProgress;
    public GameObject ProgressBar; 
    public GameObject Collider;
    //private int rand;
    public int DangerousAge;
    public string DangerousGender;
    public bool BiggerAge;
    private int st;
    private int rand;
    private int lastPatrolCrew;
    // Start is called before the first frame update
    void Start()
    {
        st = 2;
        DangerousAge = 30;
        BiggerAge = (Random.value > 0.5f);
        GameProgress = 0f;  
    }
    // Update is called once per frame
    void Update()
    {
        if(st==2)
        {
            crew = GameObject.FindGameObjectsWithTag("Character");
            SetCoordinates(crew);
            st = 1;
        }
        if(st==1)
        {
            Invoke("Patrol", 2f);
            st = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Med")
                {
                    GameProgress += 0.25f;
                    Destroy(hit.collider.gameObject);
                }
            }
        }
       
        //GameProgress += 0.001f;
        ProgressBar.GetComponent<FillBar>().CurrentValue = GameProgress;
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
    void SetCoordinates(GameObject[] crew)
    {
        Vector2[] Coordinates= new Vector2[] { new Vector2(4.35f, 2.35f), new Vector2(4f, -2f), new Vector2(-3.3f, 2.75f), new Vector2(-4f, -2f) };
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
                //crew[i].GetComponent<Character>().FirstMovementSprite = FirstMovement[i];
                //crew[i].GetComponent<Character>().LastMovementSprite = LastMovement[i];
            }
            else
            {
                if (crew[i].GetComponent<Character>().age > DangerousAge)
                    BiggerAge = true;
                else
                    BiggerAge = false;
                DangerousGender = crew[i].GetComponent<Character>().gender;
                crew[i].GetComponent<Character>().transform.position = new Vector2(0f, 0.5f);
                crew[i].GetComponent<Character>().IsInfected = true;
                crew[i].AddComponent<Infected>();
                Debug.Log(crew[i]);
            }

        }
    }
}
