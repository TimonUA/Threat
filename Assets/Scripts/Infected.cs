using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : MonoBehaviour
{
    private bool IsCollision;
    private float workCount=0.02f;
    private float currentWork;
    private float ChanceToInfect=0.02f;
    private float randN;
    private float ageChance;
    private float genderChance;
    //private int st;
    private Camera mainCamera;
    private List<GameObject> UnderThreat=new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //st = 1;
        currentWork = 0;
        mainCamera = Camera.main;
        IsCollision = false;
        gameObject.transform.parent.tag = "Infected";
        gameObject.tag = "InfectedCollider";
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<CircleCollider2D>().radius = 2.6f;
        InvokeRepeating("Disease", 0f, 1f);  
        InvokeRepeating("Threat", 2f, 1f);  
    }

    // Update is called once per frame
    void Update()
    {

    }
    void AgeImpact(int age)
    {
        int dangerousAge = mainCamera.GetComponent<Game>().dangerousAge;
        bool biggerAge = mainCamera.GetComponent<Game>().biggerAge;
        if (biggerAge == true && age > dangerousAge)
            ageChance = 3f;
        else if (biggerAge == false && age < dangerousAge)
            ageChance = 3f;
        else
            ageChance = 0f;
    }
    void GenderImpact(string gender)
    {
        string dangerousGender = mainCamera.GetComponent<Game>().dangerousGender;
        if (gender == dangerousGender)
            genderChance = 1f;
    }
    void Threat()
    {
        if(!PauseMenu.IsPaused)
        {
           gameObject.transform.parent.gameObject.GetComponent<Character>().health -= 1f;
        }
        
    }
    void Disease()
    {
        if (IsCollision)
        {
            for (int i=0;i<UnderThreat.Count;i++)
            {
                randN = Random.value;
                AgeImpact(UnderThreat[i].GetComponent<Character>().age);
                GenderImpact(UnderThreat[i].GetComponent<Character>().gender);
                Debug.Log(ChanceToInfect - UnderThreat[i].GetComponent<Character>().intelligence * 0.001f + ageChance * 0.001f + genderChance * 0.001f);
                if (randN <= ChanceToInfect - UnderThreat[i].GetComponent<Character>().intelligence * 0.001f + ageChance * 0.001f + genderChance * 0.001f)
                { 
                    //rand = Random.Range(0, UnderThreat.Count);
                    UnderThreat[i].GetComponent<Character>().IsInfected = true;
                    UnderThreat[i].transform.GetChild(0).gameObject.AddComponent<Infected>();
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
    private void Work(Character worker)
    {
        float work;
            if (worker.role == "Medic")
                work = workCount;
            else
                work = workCount / 1.5f;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "CharacterCollider")
        {
            IsCollision = true;
            UnderThreat.Add(collider.gameObject.transform.parent.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "CharacterCollider")
        {            
            if(UnderThreat.Count>1)
            {             
                UnderThreat.Remove(collider.gameObject.transform.parent.gameObject);
            }
            else
            {
                IsCollision = false;
                //UnderThreat.Remove(collider.gameObject.transform.parent.gameObject);
            }
        }
    }
}
