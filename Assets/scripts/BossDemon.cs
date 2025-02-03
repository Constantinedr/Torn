using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class BossDemon : Enemy
{
    public GameObject topHalfPrefab;
    private Vector2 projectedDashPosition;
    public GameObject Player;
    public GameObject bottomHalfPrefab;
    public float dashDuration = 0.5f;
    public float dashSpeed = 1f;
    public GameObject warningParticlePrefab; 
    private SpriteRenderer spriteRenderer;
    public GameObject warningPrefab;    
    public float dashCooldown = 10f;
    private bool canDash = true;
    private GameObject topHalfInstance;
    private GameObject bottomHalfInstance;
    public SPAWNER[] spawners;
    private bool frenzyTriggered = false;
    private bool calmTriggered = false;
    private bool splitTriggered = false;
    public float healthPercentage=100;
    private bool halvesDestroyed = false;
    public GameObject hitboxForDah;
    private BoxCollider2D hitboxColliderForDash;

    // **Health Bar Reference**
    public Image healthBarFill; 


  
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
            SpawnDamageParticles(dmg.origin, dmg.origin - (transform.position - dmg.origin).normalized);
            StartCoroutine(FlashWhite());
            LastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.up * 40, 1f);

            UpdateHealthBar();

            // Trigger Frenzy at 50% health
            if (!frenzyTriggered && hitpoint <= maxHitpoint * 0.5f && hitpoint > 0)
            {
                frenzy();
                frenzyTriggered = true;
            }
            if (!splitTriggered && hitpoint <= maxHitpoint * 0.1f)
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

private void UpdateHealthBar()
{
    if (healthBarFill != null)
    {
        healthPercentage = (float)hitpoint / maxHitpoint;
        healthBarFill.fillAmount = healthPercentage;
    }
}

    private void Update()
    {
        if (Random.Range(0, 100) < 2)
        {
            ChompChompDash();
        }

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
               
                Vector2 dashDirection = (player.transform.position - transform.position).normalized;
                projectedDashPosition = (Vector2)transform.position + dashDirection * dashSpeed * dashDuration; 

                ShowDashWarning(projectedDashPosition);

    
                StartCoroutine(FlashBeforeDash());

                canDash = false;
                Invoke(nameof(ExecuteDash), 0.4f);
            }
        }
    }

    private IEnumerator FlashBeforeDash()
    {
        for (int i = 0; i < 3; i++) // Flash 3 times before dashing
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void ExecuteDash()
    {
        hitboxColliderForDash.enabled = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 dashDirection = (projectedDashPosition - (Vector2)transform.position).normalized;
        rb.velocity = dashDirection * dashSpeed;

        Invoke(nameof(StopDash), dashDuration);
        Invoke(nameof(ResetDash), dashCooldown);
    }

    private void StopDash()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        hitboxColliderForDash.enabled = false;
    }

    private void ShowDashWarning(Vector2 position)
    {
        if (warningParticlePrefab != null)
        {
            GameObject warningEffect = Instantiate(warningParticlePrefab, position, Quaternion.identity);
            Destroy(warningEffect, 0.4f); // Auto-destroy after 0.4 seconds
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
        DisableBoss();

        topHalfInstance = Instantiate(topHalfPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        bottomHalfInstance = Instantiate(bottomHalfPrefab, transform.position + Vector3.down * 0.5f, Quaternion.identity);

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
        if (hitboxForDah != null)
            hitboxColliderForDash = hitboxForDah.GetComponent<BoxCollider2D>();
        if (hitboxColliderForDash != null)
            hitboxColliderForDash.enabled = false;
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject playerObject = GameObject.Find("PLAYER");
        if (playerObject != null)
        {
            Player = playerObject;
        }
        else
        {
            Debug.LogError("PLAYER not found in the scene!");
        }

        // **Initialize Health Bar**
        UpdateHealthBar();
    }
}
