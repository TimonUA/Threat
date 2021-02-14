using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject characterPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<6;i++)
        {
            Instantiate(characterPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
