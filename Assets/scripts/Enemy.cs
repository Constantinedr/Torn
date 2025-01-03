using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpValue = 1;
    public int goldValue;
    public GameObject coinPrefab;
    public Transform firePoint;


    public float scoreValue = 10;
    public float triggerLength = 1f;
    public float chaseLength = 5f;

    private float defaultXSpeed = 1.0f;
    private float defaultYSpeed = 0.75f;
    private float chaseXSpeed;
    private float chaseYSpeed;

    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected Animator anim; // Made protected for inheritance

    protected override void Start()
    {
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
        // Destroy enemy and grant experience to player
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
}
