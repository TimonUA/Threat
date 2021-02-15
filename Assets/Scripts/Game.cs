
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float GameProgress;
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
            rand = Random.Range(0, 6);
            Debug.Log(rand);
            Debug.Log(crew.Length);
            for(int i=0;i<6;i++)
            {
                if(i==rand)
                {
                    crew[i].GetComponent<Character>().IsInfected = true;
                    Debug.Log(crew[i]);
                    Debug.Log(crew[i].GetComponent<Character>().IsInfected);
                    st = 0;
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Med")
                {
                    GameProgress += 1f;
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
