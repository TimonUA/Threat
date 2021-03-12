using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : MonoBehaviour
{
    private bool IsCollision;
    private float ChanceToInfect=0.02f;
    private float randN;
    private float ageChance;
    private float genderChance;
    //private int st;
    private Camera mainCamera;
    private List<GameObject> UnderThreat=new List<GameObject>();
    private GameObject parantObject;
    // Start is called before the first frame update
    void Start()
    {
        //st = 1;
        mainCamera = Camera.main;
        IsCollision = false;
        parantObject = gameObject.transform.parent.gameObject;
        parantObject.GetComponent<Character>().IsInfected = true;
        gameObject.tag = "InfectedCollider";
        if (!gameObject.TryGetComponent<Rigidbody2D>(out var rigidbody2D))
            gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<CircleCollider2D>().radius = 2.6f;
        InvokeRepeating("Disease", 0f, 1f);  
        InvokeRepeating("Threat", 2f, 1f);  
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused && !Game.IsEnd)
        {
            if (parantObject.GetComponent<Character>().health < parantObject.GetComponent<Character>().maxHealth * 0.4 && parantObject.tag == "MainInfected")
                gameObject.GetComponent<CircleCollider2D>().radius = 6.6f;
            else if (parantObject.GetComponent<Character>().health < parantObject.GetComponent<Character>().maxHealth * 0.2 && parantObject.tag == "MainInfected")
                ChanceToInfect *= 1.5f;
            Debug.Log(ChanceToInfect);
        }
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
        if(!PauseMenu.IsPaused && !Game.IsEnd)
        {
            parantObject.gameObject.GetComponent<Character>().health -= 1f;
        }
        
    }
    void Disease()
    {
        if (IsCollision && !PauseMenu.IsPaused && !Game.IsEnd)
        {
                for (int i = 0; i < UnderThreat.Count; i++)
                {
                    UnderThreat[i].GetComponent<Character>().workTime = 1f;
                    randN = Random.value;
                    AgeImpact(UnderThreat[i].GetComponent<Character>().age);
                    GenderImpact(UnderThreat[i].GetComponent<Character>().gender);
                    //Debug.Log(ChanceToInfect - UnderThreat[i].GetComponent<Character>().intelligence * 0.001f + ageChance * 0.001f + genderChance * 0.001f);
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
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "CharacterCollider" || collider.tag == "InfectedCollider")
        {
            IsCollision = true;
            UnderThreat.Add(collider.gameObject.transform.parent.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "CharacterCollider" || collider.tag == "InfectedCollider")
        {            
            if(UnderThreat.Count>1)
            {             
                UnderThreat.Remove(collider.gameObject.transform.parent.gameObject);
            }
            else
            {
                IsCollision = false;
                UnderThreat.Remove(collider.gameObject.transform.parent.gameObject);
            }
        }
    }
}
