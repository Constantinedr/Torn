using System.Collections;
using UnityEngine;

public class round : Collectable
{
    public Sprite activatedSprite; // The sprite to change to after activation
    public SPAWNER[] spawners; // References to the spawners
    public GameObject[] walls; // Array to hold references to Wall GameObjects
    public GameObject DESTROYwalls; // Reference to the GameObject to destroy walls
    public string ACTIVEALL;
    public string IDLE; // The name of the Wall animation trigger

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = activatedSprite;

            // Trigger Activate on all spawners
            if (spawners != null && spawners.Length > 0)
            {
                foreach (SPAWNER spawner in spawners)
                {
                    spawner.TriggerActivate();
                }
            }

            // Trigger CalmActivate 15 seconds later
            StartCoroutine(TriggerCalmActivateAfterDelay());

            // Trigger animations on all Wall GameObjects
            if (walls != null && walls.Length > 0)
            {
                foreach (GameObject wall in walls)
                {
                    if (wall != null)
                    {
                        Animator wallAnimator = wall.GetComponent<Animator>();
                        if (wallAnimator != null)
                        {
                            wallAnimator.SetTrigger(ACTIVEALL);
                        }
                        else
                        {
                            Debug.LogWarning($"Wall '{wall.name}' does not have an Animator component.");
                        }
                    }
                }
            }

            GameManager.instance.ShowText("begin", 25, Color.yellow, transform.position, Vector3.up * 50, 3.0f);
        }
    }

    private IEnumerator TriggerCalmActivateAfterDelay()
    {
        yield return new WaitForSeconds(15f); // Wait for 15 seconds

        // Trigger CalmActivate on all spawners
        if (spawners != null && spawners.Length > 0)
        {
            foreach (SPAWNER spawner in spawners)
            {
                spawner.TriggerCalmActivate();
            }
        }

        // Check for GameObjects with the Enemy script
        FASTDEMON[] enemies = FindObjectsOfType<FASTDEMON>();
        if (enemies.Length == 0)
        {
            DestroyWall();
        }
    }

    private void DestroyWall()
    {
        if (DESTROYwalls != null)
        {   
            // Call destroyWall on the DESTROYwalls GameObject
            DESTROYwalls.SendMessage("destroyWall", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            Debug.LogWarning("DESTROYwalls reference is not set!");
        }
    }
}
