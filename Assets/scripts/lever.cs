using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Sprite activatedSprite; // The sprite to change to after activation
    public GameObject wall; // Reference to the wall GameObject
    
    private SpriteRenderer spriteRenderer;
    private bool isActivated = false; // Prevent multiple activations

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on the Lever!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding GameObject is the PLAYER and the lever is not already activated
        if (collision.CompareTag("PLAYER") && !isActivated)
        {
            ActivateLever();
        }
    }

    private void ActivateLever()
    {
        isActivated = true; // Mark the lever as activated

        // Change the sprite to the activated version
        if (activatedSprite != null)
        {
            spriteRenderer.sprite = activatedSprite;
        }
        else
        {
            Debug.LogWarning("Activated sprite is not set!");
        }

        // Interact with the wall
        if (wall != null)
        {
            wall wallScript = wall.GetComponent<wall>();
            if (wallScript != null)
            {
                wallScript.destroyWall(); // Destroy the wall
            }
            else
            {
                Debug.LogError("The referenced wall does not have the 'wall' script attached!");
            }
        }
        else
        {
            Debug.LogWarning("Wall reference is not set!");
        }
    }
}
