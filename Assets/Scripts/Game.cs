
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float GameProgress;
    public GameObject ProgressBar; 
    public GameObject Collider;
    private int rand;
    private int st;
    // Start is called before the first frame update
    void Start()
    {
        st = 1;
        GameProgress = 0f;  
    }
    // Update is called once per frame
    void Update()
    {
        if(st==1)
        {
            GameObject[] crew = GameObject.FindGameObjectsWithTag("Character");
            SetCoordinates(crew);
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
    void SetCoordinates(GameObject[] crew)
    {
        Vector2[] Coordinates= new Vector2[] {new Vector2(6.0f, -1f), new Vector2(-5f, -1.5f), new Vector2(4.35f, 2.35f), new Vector2(3f, -2.5f), new Vector2(-3.3f, 2.75f), new Vector2(-3f, -2.5f) };
        for (int i=0;i<crew.Length;i++)
        {
            if(i<Coordinates.Length)
                crew[i].GetComponent<Character>().transform.position = Coordinates[i];
            else
            {
                crew[i].GetComponent<Character>().transform.position = new Vector2(0f, 0.5f);
                crew[i].GetComponent<Character>().IsInfected = true;
                crew[i].AddComponent<Infected>();
                Debug.Log(crew[i]);
            }

        }
    }
}
