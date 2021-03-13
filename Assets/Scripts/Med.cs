using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Med : MonoBehaviour
{
    public float LifeTimeRemain = 3f;
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused && !Game.IsEnd && !DialogueManager.IsDialogue)
        {
            Destroy(this.gameObject, LifeTimeRemain);
        }
    }
}
