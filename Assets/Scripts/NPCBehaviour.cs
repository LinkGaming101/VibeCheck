using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Animate();    
    }

    void Animate()
    {
        // animation code goes here for idle and constant if any
    }

    public void Dialogue()
    {
        //start the NPC dialogue
        Debug.Log("Dialogue started!");
    }
}
