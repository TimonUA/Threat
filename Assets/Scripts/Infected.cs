using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : MonoBehaviour
{
    private bool IsCollision;
    private float ChanceToInfect=0.002f;
    private float randN;
    private int rand;
    private float ageChance;
    //private int st;
    private Camera mainCamera;
    private List<GameObject> UnderThreat=new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //st = 1;
        mainCamera = Camera.main;
        IsCollision = false;
        gameObject.tag = "Infected";
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<CircleCollider2D>().radius = 2.6f;
        InvokeRepeating("Disease", 0f, 2f);  
    }

    // Update is called once per frame
    void Update()
    {

    }
    void AgeImpact(int age)
    {
        int DangerousAge = mainCamera.GetComponent<Game>().DangerousAge;
        bool BiggerAge = mainCamera.GetComponent<Game>().BiggerAge;
        if (BiggerAge == true && age > DangerousAge)
            ageChance = 3f;
        else if (BiggerAge == false && age < DangerousAge)
            ageChance = 3f;
        else
            ageChance = 0f;
    }
    void Disease()
    {
        if (IsCollision)
        {
            for (int i=0;i<UnderThreat.Count;i++)
            {
                randN = Random.value;
                AgeImpact(UnderThreat[i].GetComponent<Character>().age);
                Debug.Log(ChanceToInfect - UnderThreat[i].GetComponent<Character>().intelligence * 0.0001f - ageChance * 0.0001f);
                if (randN <= ChanceToInfect - UnderThreat[i].GetComponent<Character>().intelligence * 0.0001f - ageChance * 0.0001f)
                { 
                    //rand = Random.Range(0, UnderThreat.Count);
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
            //Debug.Log(UnderThreat.Count);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Character")
        {
            IsCollision = true;
            UnderThreat.Add(collider.gameObject);
        }
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
