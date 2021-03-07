using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : MonoBehaviour
{
    private bool IsCollision;
    // Start is called before the first frame update
    void Start()
    {
        IsCollision = false;
        gameObject.tag = "Infected";
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<BoxCollider2D>().edgeRadius = 1.2f;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(3f, 1f);
        InvokeRepeating("Test", 0f, 2f);
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
        }
    }
    void Test()
    {
        if(IsCollision)
        Debug.Log("Trigger");
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Character")
        {            
            IsCollision = false;
        }
    }
}
