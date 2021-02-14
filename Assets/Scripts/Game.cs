using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float GameProgress;

    // Start is called before the first frame update
    void Start()
    {
        GameProgress = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //GameProgress += 0.001f;
        //Debug.Log(GameProgress);
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
