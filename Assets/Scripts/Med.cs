using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Med : MonoBehaviour
{
    public float LifeTimeRemain = 2f;
    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject,LifeTimeRemain);
    }
}
