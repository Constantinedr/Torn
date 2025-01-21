using UnityEngine;

public class BossDemon : Enemy
{   
    public GameObject topHalfPrefab;  // Assign in Inspector
    public GameObject Player; 
    public GameObject bottomHalfPrefab; // Assign in Inspector
    public float splitDuration = 5f;  // Time before reforming
    public float dashSpeed = 1f; // Speed of the dash
    public float dashCooldown = 10f; // Cooldown between dashes
    private bool canDash = true; // To control dash cooldown    
    private GameObject topHalfInstance;
    private GameObject bottomHalfInstance;
    public SPAWNER[] spawners; // Array to hold references to the spawners
    private bool frenzyTriggered = false; // Ensure Frenzy triggers only once
    private bool calmTriggered = false;   // Ensure Calm triggers only once
    private bool SplitTriggered = false;

    // Override the animation state method
    protected override void SetAnimationState(bool isWalking)
    {
        if (anim != null)
        {
            anim.SetBool("BossDemonWalk", isWalking);
        }
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (Time.time - LastImmune > immuneTime)
        {
            LastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.up * 40, 1f);

            // Trigger Frenzy at 50% health
            if (!frenzyTriggered && hitpoint <= maxHitpoint * 0.5 && hitpoint > 0)
            {
                frenzy();
                frenzyTriggered = true;
            }
            if (!SplitTriggered && hitpoint <= maxHitpoint * 0.25 && hitpoint > 0)
            {
                Split();
                SplitTriggered = true;
            }
            // Trigger Calm at near max health
            if (!calmTriggered && hitpoint >= maxHitpoint - 1)
            {
                calm();
                calmTriggered = true;
            }

            // Handle Death
            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }
     private void Update()
    {
        // Randomly trigger dash if possible
        if (Random.Range(0, 100) < 2)  // 2% chance per frame to dash
        {
            ChompChompDash();
        }
        
    }
      private void ResetDash()
    {
        canDash = true;
    }

  protected void ChompChompDash()
    {
        if (canDash)
        {
            GameObject player = Player;

            if (player != null)
            {
                Debug.Log("Boss is dashing towards the player!");

                Vector2 dashDirection = (player.transform.position - transform.position).normalized;

                GetComponent<Rigidbody2D>().velocity = dashDirection * dashSpeed;

                canDash = false;
                Invoke(nameof(ResetDash), dashCooldown);
            }
        }
    }

    protected virtual void frenzy()
    {
        if (spawners != null && spawners.Length > 0)
        {
            foreach (SPAWNER spawner in spawners)
            {
                spawner.TriggerActivate();
            }
        }
    }
     protected virtual void Split()
    {
        Debug.Log("Boss is splitting!");

        Destroy(gameObject);
        // Instantiate split parts
        topHalfInstance = Instantiate(topHalfPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        bottomHalfInstance = Instantiate(bottomHalfPrefab, transform.position + Vector3.down * 0.5f, Quaternion.identity);

        // Apply movement to the halves (e.g., moving in opposite directions)
        topHalfInstance.GetComponent<Rigidbody2D>().velocity = Vector2.up * 2f;
        bottomHalfInstance.GetComponent<Rigidbody2D>().velocity = Vector2.down * 2f;

 
    }



    protected virtual void calm()
    {
        if (spawners != null && spawners.Length > 0)
        {
            foreach (SPAWNER spawner in spawners)
            {
                spawner.TriggerCalmActivate();
            }
        }
    }
}
