using System.Collections;
using UnityEngine;

public class round : Collectable
{
    public Sprite activatedSprite; // The sprite to change to after activation
    public SPAWNER[] spawners; // References to the spawners

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

            // Trigger CalmActivate 10 seconds later
            StartCoroutine(TriggerCalmActivateAfterDelay());

            GameManager.instance.ShowText("begin", 25, Color.yellow, transform.position, Vector3.up * 50, 3.0f);
        }
    }

    private IEnumerator TriggerCalmActivateAfterDelay()
    {
        yield return new WaitForSeconds(10f); // Wait for 10 seconds

        if (spawners != null && spawners.Length > 0)
        {
            foreach (SPAWNER spawner in spawners)
            {
                spawner.TriggerCalmActivate(); // Trigger CalmActivate
            }
        }
    }
}
