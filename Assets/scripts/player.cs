using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{

    private HeartManager heartManager;
    [SerializeField] private float speedBuffMultiplier = 1.5f; // How much faster the player moves during the buff
    private bool isSpeedBuffActive = false;

    protected override void Start()
    {
        hitpoint= maxHitpoint;

        heartManager = FindObjectOfType<HeartManager>();
        heartManager.InitializeHearts(maxHitpoint);
        heartManager.UpdateHearts(hitpoint);
        base.Start();
    }

    private void FixedUpdate()
    {
        
        
        heartManager.UpdateHearts(hitpoint);
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Check for speed buff input
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
    }

    private void ActivateSpeedBuff()
    {
        if (!isSpeedBuffActive)
        {
            xSpeed *= speedBuffMultiplier;
            ySpeed *= speedBuffMultiplier;
            isSpeedBuffActive = true;
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

public void Heal(float amount)
{
    if (hitpoint<maxHitpoint){
    int healAmount = Mathf.RoundToInt(amount); // Convert float to int
    hitpoint += healAmount;
    hitpoint = Mathf.Clamp(hitpoint, 0, maxHitpoint); // Ensure hitpoint stays within bounds

    GameManager.instance.ShowText(healAmount.ToString(), 25, Color.green, transform.position, Vector3.up * 40, 1f);

    heartManager.UpdateHearts(hitpoint);
    }
}

protected override void ReceiveDamage(Damage dmg)
{
    if (Time.time - LastImmune > immuneTime)
    {
        LastImmune = Time.time;
        hitpoint -= dmg.damageAmount;
        pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

        // Removed the text display when the player takes damage
        // GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.up * 40, 1f);

        if (hitpoint <= 0)
        {
            hitpoint = 0;
            Death();
        }
    }
}
  
    
}
