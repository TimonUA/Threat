using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    float GameProgress;
    // Start is called before the first frame update
    void Start()
    {
        GameProgress = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        GameProgress += 0.001f;
        Debug.Log(GameProgress);
    }
}
