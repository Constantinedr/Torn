using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : Collidable
{
    public float healingAmount = 1; // Set this in the inspector or via code

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "PLAYER")
        {
            coll.SendMessage("Heal", healingAmount);
        }
    }
}

