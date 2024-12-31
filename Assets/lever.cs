using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverForDoor : Collectable
{
    public Sprite activatedSprite; // The sprite to change to after activation
    public GameObject wall; // Reference to the wall GameObject

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = activatedSprite;

            if (wall != null)
            {
                wall wallScript = wall.GetComponent<wall>(); // Correct class name
                if (wallScript != null)
                {
                    wallScript.destroyWall(); // Call the method to destroy the wall
                }
            }

            GameManager.instance.ShowText("Opened the door", 25, Color.yellow, transform.position, Vector3.up * 50, 3.0f);
        }
    }
}
