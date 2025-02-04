using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpValue = 1;
    public round roundScript;
    public int goldValue;
    [SerializeField] private ParticleSystem damageParticles;
    private ParticleSystem damageParticlesInstance;
    public GameObject coinPrefab;
    private SpriteRenderer spriteRenderer;
    public Transform firePoint;


    public int scoreValue = 10;
    public float triggerLength = 1f;
    public float chaseLength = 5f;

    public float defaultXSpeed = 1.0f;
    public float defaultYSpeed = 0.75f;
    public float chaseXSpeed;
    public float chaseYSpeed;

    public bool chasing;
    public bool collidingWithPlayer;
    public Transform playerTransform;
    private float staggerDuration = 0.5f;
    public Vector3 startingPosition;

    public ContactFilter2D filter;
    public BoxCollider2D hitbox;
    public Collider2D[] hits = new Collider2D[10];
    public int difficulty;
    private GameObject difficultyCounter;
    protected Animator anim; // Made protected for inheritance
    private int buff;
    

    public IEnumerator FlashWhite()
{
    if (spriteRenderer != null)
    {
        spriteRenderer.color = Color.white; // Change to white
        yield return new WaitForSeconds(0.1f); // Wait briefly
        spriteRenderer.color = Color.red; // Slight red tint to indicate damage
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white; // Back to normal
    }
}

    protected override void Start()
    {   
        roundScript = FindObjectOfType<round>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        difficultyCounter = GameObject.Find("COUNTER");
        if (difficultyCounter != null)
        {
            COUNTER counterScript = difficultyCounter.GetComponent<COUNTER>();
            if (counterScript != null && counterScript.scoreText != null)
            {
                if (int.TryParse(counterScript.scoreText.text, out difficulty))
                {
                    difficulty = Mathf.Max(difficulty, 1); // Ensure difficulty is at least 1
                }
                else
                {
                    difficulty = 1;
                }
            }
        }
        else
        {
            difficulty = 1;
        }
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();

        // Set chase speeds based on initial speed values
        chaseXSpeed = xSpeed;
        chaseYSpeed = ySpeed;

        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError($"Animator is missing on {gameObject.name}. Please attach one.");
        }
            buff = Mathf.RoundToInt(maxHitpoint * difficulty * 0.5f); // Ensures integer value
            maxHitpoint += buff;
            hitpoint = maxHitpoint;

    }

    private void FixedUpdate()
    {
        HandleChasing();

        // Reset collision detection
        collidingWithPlayer = false;

        // Check for collisions
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].CompareTag("FIGHTER") && hits[i].name == "PLAYER")
            {
                collidingWithPlayer = true;
            }

            // Clear processed hit
            hits[i] = null;
        }
    }

    private void HandleChasing()
    {
        float distanceToPlayer = Vector3.Distance(playerTransform.position, startingPosition);

        if (distanceToPlayer < chaseLength)
        {
            if (distanceToPlayer < triggerLength)
            {
                chasing = true;
            }

            if (chasing)
            {
                // Set speeds for chasing
                xSpeed = chaseXSpeed;
                ySpeed = chaseYSpeed;

                if (!collidingWithPlayer)
                {
                    // Chase the player
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
                else
                {
                    // Stop movement when colliding
                    UpdateMotor(Vector3.zero);
                }

                // Play walking animation
                SetAnimationState(true);
            }
            else
            {
                // Return to starting position
                UpdateMotor(startingPosition - transform.position);
                SetAnimationState(false);
            }
        }
        else
        {
            // Stop chasing if the player is out of range
            chasing = false;
            UpdateMotor(startingPosition - transform.position);

            // Reset to default speeds
            xSpeed = defaultXSpeed;
            ySpeed = defaultYSpeed;

            // Stop walking animation
            SetAnimationState(false);
        }
    }

    // Virtual method for setting animation state
    protected virtual void SetAnimationState(bool isWalking)
    {
        if (anim != null)
        {
            anim.SetBool("IsWalking", isWalking);
        }
    }

    protected override void Death()
    {
        if (roundScript != null){
            roundScript.EnemyDied();
        }
        Destroy(gameObject);
        for(int i=0; i<=goldValue-1; i++){
        GameObject gold = Instantiate(coinPrefab, firePoint.position, firePoint.rotation);
        }
        GameManager.instance.experience += xpValue;
        GameManager.instance.score += scoreValue;
        GameManager.instance.ShowText(
            "+" + xpValue + " XP",
            30,
            Color.magenta,
            playerTransform.position,
            Vector3.up * 40,
            1.0f
        );
    }
        private IEnumerator StaggerEffect()
    {
        
       
        xSpeed = 0;
        ySpeed = 0;
        yield return new WaitForSeconds(staggerDuration);
        xSpeed = defaultXSpeed;
        ySpeed = defaultYSpeed;

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
                anim.SetTrigger("Stagger");
                StartCoroutine(StaggerEffect());
                GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.white, transform.position,Vector3.up*10, 0.5f);

                if (hitpoint <=0){
                    hitpoint = 0;
                    Death ();
                }
        }
    }    
    public void SpawnDamageParticles(Vector3 hitPosition, Vector3 attackOrigin)
    {
        // Calculate direction from attack origin to hit position
        Vector3 attackDirection = (hitPosition - attackOrigin).normalized;

        // Ensure attackDirection is valid
        if (attackDirection == Vector3.zero)
            attackDirection = Vector3.right; // Default to right if the direction is somehow (0,0)

        // Offset the position slightly behind the enemy
        Vector3 spawnPosition = hitPosition - (attackDirection * -0.3f); // Adjust '0.3f' for desired distance
        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Instantiate(damageParticles, spawnPosition, rotation);
    }
}
