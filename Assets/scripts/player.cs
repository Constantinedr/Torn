using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private Animator anim;
    private bool isAlive = true;
    private HeartManager heartManager;
    private float speedBuffMultiplier = 1.2f;
    private float speedReductionMultiplier = 0.4f; // Reduction multiplier for slower movement
    private bool isSpeedBuffActive = false;
    private bool isSpeedReductionActive = false;

    private int lastHitpoint; // Store previous HP for change detection

    protected override void Start()
    {
        anim = GetComponent<Animator>(); // Ensure this is correct
        hitpoint = maxHitpoint;
        lastHitpoint = hitpoint;

        heartManager = FindObjectOfType<HeartManager>();
        if (heartManager != null)
        {
            heartManager.InitializeHearts(maxHitpoint);
            heartManager.UpdateHearts(hitpoint);
        }

        base.Start();
        DontDestroyOnLoad(gameObject);



        GameObject spawner = GameObject.Find("SPAWN");

        if (spawner != null)
        {
            // Teleport this GameObject to the position of the Spawner
            transform.position = spawner.transform.position;
            Debug.Log($"{gameObject.name} teleported to Spawner at {spawner.transform.position}");
        }

    }

    private void FixedUpdate()
    {
        // Check for HP changes
        if (hitpoint != lastHitpoint)
        {
            OnHPChanged();
            lastHitpoint = hitpoint; // Update lastHitpoint
        }
        if (isAlive){
        heartManager?.UpdateHearts(hitpoint);
        
        HandleMovement();
        }
    }

    private void HandleMovement()
    {
        // Handle speed reduction when right mouse button is held
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            ActivateSpeedReduction();
        }
        else
        {
            DeactivateSpeedReduction();
        }

        // Handle speed buff when space key is held
        if (Input.GetKey(KeyCode.Space))
        {
            ActivateSpeedBuff();
        }
        else
        {
            DeactivateSpeedBuff();
        }

        // Get movement input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDelta = new Vector3(x, y, 0);

        // Apply movement and handle collisions
        UpdateMotor(moveDelta);

        // Update animation based on movement input
        UpdateMovementAnimation(x, y);
    }

    private void UpdateMovementAnimation(float x, float y)
    {
        if (x != 0 || y != 0)
        {
            // Player is moving
            anim.SetBool("IsWalking2", true);  // Play "IsWalking2" animation
        }
        else
        {
            // Player is not moving
            anim.SetBool("IsWalking2", false); // Stop the walking animation
        }
    }

    private void ActivateSpeedBuff()
    {
        if (!isSpeedBuffActive && !isSpeedReductionActive)
        {
            xSpeed *= speedBuffMultiplier;
            ySpeed *= speedBuffMultiplier;
            isSpeedBuffActive = true;
        }
    }

    private void ActivateSpeedReduction()
    {
        if (!isSpeedReductionActive && !isSpeedBuffActive)
        {
            xSpeed *= speedReductionMultiplier;
            ySpeed *= speedReductionMultiplier;
            isSpeedReductionActive = true;
        }
    }

    private void DeactivateSpeedBuff()
    {
        if (isSpeedBuffActive)
        {
            xSpeed /= speedBuffMultiplier;
            ySpeed /= speedBuffMultiplier;
            isSpeedBuffActive = false;
        }
    }

    private void DeactivateSpeedReduction()
    {
        if (isSpeedReductionActive)
        {
            xSpeed /= speedReductionMultiplier;
            ySpeed /= speedReductionMultiplier;
            isSpeedReductionActive = false;
        }
    }

    public void Heal(float amount)
    {
        if (hitpoint < maxHitpoint)
        {
            int healAmount = Mathf.RoundToInt(amount); // Convert float to int
            hitpoint += healAmount;
            hitpoint = Mathf.Clamp(hitpoint, 0, maxHitpoint); // Ensure hitpoint stays within bounds

            GameManager.instance.ShowText(healAmount.ToString(), 25, Color.green, transform.position, Vector3.up * 40, 1f);

            heartManager?.UpdateHearts(hitpoint);
        }
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (Time.time - LastImmune > immuneTime)
        {
            LastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }

    private void OnHPChanged()
    {
        if (hitpoint > lastHitpoint)
        {
            // HP increased
            anim.SetTrigger("healler");
        }
        else if (hitpoint < lastHitpoint)
        {
            // HP decreased
            anim.SetTrigger("rel");
        }
    }
    
    protected override void Death(){
        isAlive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
    }

}

