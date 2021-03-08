using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : MonoBehaviour
{
    private bool IsCollision;
    private float ChanceToInfect;
    private int rand;
    private List<GameObject> UnderThreat=new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        IsCollision = false;
        gameObject.tag = "Infected";
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<CircleCollider2D>().radius = 2.6f;
        InvokeRepeating("Test", 0f, 2f);
        InvokeRepeating("Disease", 0f, 2f);      
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Character")
        {         
            IsCollision = true;
            UnderThreat.Add(collider.gameObject);
        }
    }
    void Test()
    {
        if (IsCollision)
        {
            ChanceToInfect = Random.value;
            for (int i=0;i<UnderThreat.Count;i++)
            {
                if (ChanceToInfect < 0.2)
                {
                    rand = Random.Range(0, UnderThreat.Count);
                    UnderThreat[i].GetComponent<Character>().IsInfected = true;
                    UnderThreat[i].AddComponent<Infected>();
                    if (UnderThreat.Count > 1)
                    {
                        UnderThreat.Remove(UnderThreat[i]);
                    }
                    else
                    {
                        IsCollision = false;
                        UnderThreat.Remove(UnderThreat[i]);
                    }

                }
            }         
            Debug.Log(UnderThreat.Count);
        }
    }
    void Disease()
    {

    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Character")
        {            
            if(UnderThreat.Count>1)
            {             
                UnderThreat.Remove(collider.gameObject);
            }
            else
            {
                IsCollision = false;
                UnderThreat.Remove(collider.gameObject);
            }
        }
    }
}
