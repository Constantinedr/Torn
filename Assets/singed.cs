using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singed : Mover
{
    public int xpValue = 1;
    public round roundScript;
    public int goldValue;
    [SerializeField] private ParticleSystem damageParticles;
    private ParticleSystem damageParticlesInstance;
    public GameObject coinPrefab;
    private SpriteRenderer spriteRenderer;
    public Transform firePoint;
    private TrailRenderer trailRenderer; // Trail Renderer component

    public int scoreValue = 10;
    public float moveSpeed = 1.0f;
    public float changeDirectionTime = 2f; // Time interval for changing direction
    private Vector3 moveDirection;
    private float timer;

    private BoxCollider2D hitbox;
    private Animator anim;
    private int difficulty;

    protected override void Start()
    {   
        roundScript = FindObjectOfType<round>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        base.Start();
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>(); // Get Trail Renderer component

        if (anim == null)
        {
            Debug.LogError($"Animator is missing on {gameObject.name}. Please attach one.");
        }
        
        if (trailRenderer == null)
        {
            Debug.LogError($"TrailRenderer is missing on {gameObject.name}. Please attach one.");
        }
        else
        {
            trailRenderer.enabled = true; // Enable Trail Renderer
        }

        moveDirection = GetRandomDirection();
        timer = changeDirectionTime;
    }

    private void FixedUpdate()
    {
        HandleRandomMovement();
    }

    private void HandleRandomMovement()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0 || IsCollidingWithWall())
        {
            moveDirection = GetRandomDirection();
            timer = changeDirectionTime;
        }

        UpdateMotor(moveDirection);
        SetAnimationState(moveDirection != Vector3.zero);
    }

    private Vector3 GetRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        return new Vector3(randomX, randomY).normalized;
    }

    private bool IsCollidingWithWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, 0.5f, LayerMask.GetMask("Wall"));
        return hit.collider != null;
    }

    protected virtual void SetAnimationState(bool isWalking)
    {
        if (anim != null)
        {
            anim.SetBool("IsWalking", isWalking);
        }
    }

    protected override void Death()
    {
        if (roundScript != null)
        {
            roundScript.EnemyDied();
        }
        Destroy(gameObject);
        for (int i = 0; i <= goldValue - 1; i++)
        {
            Instantiate(coinPrefab, firePoint.position, firePoint.rotation);
        }
        GameManager.instance.experience += xpValue;
        GameManager.instance.score += scoreValue;
    }
}
