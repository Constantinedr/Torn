using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : Collidable
{
    public COUNTER counter;

    private void Start()
    {
        // Find the COUNTER object in the scene
        counter = FindObjectOfType<COUNTER>();

        if (counter == null)
        {
            Debug.LogError("No COUNTER script found in the scene!");
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "PLAYER" )
        {
            counter.AddScore(); 
            Debug.Log("ji");
        }
    }
}
