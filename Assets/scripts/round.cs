using System.Collections;
using UnityEngine;

public class round : Collectable
{
    public Sprite activatedSprite; // The sprite to change to after activation
    public SPAWNER[] spawners; // References to the spawners
    public GameObject[] walls; // Array to hold references to Wall GameObjects
    public GameObject DESTROYwalls; // Reference to the GameObject to destroy walls
    public string ACTIVEALL;
    public int enemyCount = 8;
    public string IDLE; // The name of the Wall animation trigger
    public void EnemyDied()
    {
        if (enemyCount > 0)
        {
            enemyCount--;
        }
        if (enemyCount <= 0)
        {
            DestroyWall();
        }
    }
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


            StartCoroutine(TriggerCalmActivateAfterDelay());
  
  

            GameManager.instance.ShowText("begin", 25, Color.yellow, transform.position, Vector3.up * 50, 3.0f);
        }
    }

    private IEnumerator TriggerCalmActivateAfterDelay()
    {
        yield return new WaitForSeconds(20f); // Wait for 15 seconds

        // Trigger CalmActivate on all spawners
        if (spawners != null && spawners.Length > 0)
        {
            foreach (SPAWNER spawner in spawners)
            {
                spawner.TriggerCalmActivate();
            }
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
