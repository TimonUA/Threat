using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private Vector3[] waypoints;
    private int current;
    public float speed;
    private bool IsPatrol;
    public float TimeInCheck = 3f;
    private Camera mainCamera;
    private Game game;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = new Vector3[] { gameObject.GetComponent<Character>().startPosition, gameObject.GetComponent<Character>().gatePosition, gameObject.GetComponent<Character>().endPosition };
        current = 0;
        speed = 2;
        IsPatrol = true;
        mainCamera = Camera.main;
        game = mainCamera.GetComponent<Game>();
        //InvokeRepeating("Patroling", 0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        Patroling();
        if (TimeInCheck > 0)
        {
            if (transform.position == waypoints[2])
            {
                IsPatrol = false;
                TimeInCheck -= Time.deltaTime;
            }
        }
        else
        {
            IsPatrol = true;
            if(transform.position==waypoints[0])
            {
                IsPatrol = false;
                Destroy(this);
                game.Test();
            }
        }
    }
    void Patroling()
    {
        if (IsPatrol)
        {
            if (transform.position != waypoints[current])
                transform.position = Vector3.MoveTowards(transform.position, waypoints[current], speed * Time.deltaTime);
            else
                current = (current + 1) % waypoints.Length;
        }
    }
}
