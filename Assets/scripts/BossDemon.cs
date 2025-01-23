using UnityEngine;

public class BossDemon : Enemy
{
    public GameObject topHalfPrefab;  // Assign in Inspector
    public GameObject Player;
    public GameObject bottomHalfPrefab; // Assign in Inspector
    public float dashDuration = 0.5f;
    public float dashSpeed = 1f; // Speed of the dash
    public float dashCooldown = 10f; // Cooldown between dashes
    private bool canDash = true; // To control dash cooldown    
    private GameObject topHalfInstance;
    private GameObject bottomHalfInstance;
    public SPAWNER[] spawners; // Array to hold references to the spawners
    private bool frenzyTriggered = false; // Ensure Frenzy triggers only once
    private bool calmTriggered = false;   // Ensure Calm triggers only once
    private bool splitTriggered = false;
    private bool halvesDestroyed = false;

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
            if (!frenzyTriggered && hitpoint <= maxHitpoint * 0.5f && hitpoint > 0)
            {
                frenzy();
                frenzyTriggered = true;
            }
            if (!splitTriggered && hitpoint <= maxHitpoint * 0.1f && hitpoint > 0)
            {
                Split();
                splitTriggered = true;
            }
            // Trigger Calm at near max health
            if (!calmTriggered && hitpoint >= maxHitpoint - 1)
            {
                calm();
                calmTriggered = true;
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

        // Check if both halves are destroyed to destroy the main boss
        if (splitTriggered)
        {
            Destroy(gameObject);
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
                // Calculate the dash direction
                Vector2 dashDirection = (player.transform.position - transform.position).normalized;

                // Dash movement
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.velocity = dashDirection * dashSpeed;

                // Disable dashing ability until cooldown is complete
                canDash = false;

                // Stop the dash after the duration
                Invoke(nameof(StopDash), dashDuration);

                // Reset dash ability after cooldown
                Invoke(nameof(ResetDash), dashCooldown);
            }
        }
    }

    private void StopDash()
    {
        // Stop the boss's movement by setting velocity to zero
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
        // Disable the boss's sprite and colliders instead of destroying it
        DisableBoss();

        // Instantiate top and bottom halves
        topHalfInstance = Instantiate(topHalfPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        bottomHalfInstance = Instantiate(bottomHalfPrefab, transform.position + Vector3.down * 0.5f, Quaternion.identity);

        // Apply movement to the halves (e.g., moving in opposite directions)
        topHalfInstance.GetComponent<Rigidbody2D>().velocity = Vector2.up * 2f;
        bottomHalfInstance.GetComponent<Rigidbody2D>().velocity = Vector2.down * 2f;
    }

    private void DisableBoss()
    {
         Destroy(gameObject);
        GetComponent<SpriteRenderer>().enabled = false;
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Disable sprite renderers and colliders of all children
        foreach (Transform child in transform)
        {
            SpriteRenderer childSprite = child.GetComponent<SpriteRenderer>();
            if (childSprite != null)
            {
                childSprite.enabled = false;
            }

            Collider2D childCollider = child.GetComponent<Collider2D>();
            if (childCollider != null)
            {
                childCollider.enabled = false;
            }
        }
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

protected override void Start()
{
    base.Start();  // Call the base class Start() if needed

    GameObject playerObject = GameObject.Find("PLAYER");
    if (playerObject != null)
    {
        Player = playerObject;
    }
    else
    {
        Debug.LogError("PLAYER not found in the scene!");
    }
}
}