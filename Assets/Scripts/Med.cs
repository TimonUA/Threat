using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Med : MonoBehaviour
{
    //public float progres;
    
    public float LifeTimeRemain = 2f;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject progressGO = GameObject.Find("MainCamera");
        //Game game = progressGO.GetComponent<Game>();
        //progres = game.GameProgress;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject,LifeTimeRemain);
        //if (Input.GetMouseButtonDown(0))
        //{
        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //if (hit.collider != null)
        //{
        //    if (hit.collider.gameObject == this.gameObject)
        //    {
        //        progres += 1f;
        //        Destroy(this.gameObject);
        //    }
        //}
        //}
    }
}
