using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //public string nick;
    public string gender;
    public string role;
    public int age;
    public int intelligence;
    public int strength;
    private int maxStats = 10;
    private int rand;
    private int typeName;
    public bool IsInfected;
    //public Character()
    void Start()
    {
        IsInfected = false;
        rand = Random.Range(0, 2);
        if (rand == 1)
        {
            gender = "Male";
            typeName = 6;
            name = NVJOBNameGen.Uppercase(NVJOBNameGen.GiveAName(typeName));
        }
        else
        {
            gender = "Female";
            typeName = 5;
            name = NVJOBNameGen.Uppercase(NVJOBNameGen.GiveAName(typeName));
        }
        age = Random.Range(18, 51);
        rand = Random.Range(0, 3);
        if (rand == 0)
        {
            role = "Engineer";
        }
        else if (rand == 1)
        {
            role = "Medic";
        }
        else
        {
            role = "Explorer";
        }
            for (int i = 0; i < maxStats; i++)
        {
            rand = Random.Range(0, 2);

            if (rand == 0)
            {
                intelligence += 1;
            }
            else
            {
                strength += 1;
            }
        }
    }
    //Update is called once per frame
    void Update()
    {

    }
}
