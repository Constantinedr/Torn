using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demonShooter : Enemy
{
    public GameObject projectile;
    public float cooldown = 3f;
    public float lastTimeFired;

    public bool CanShoot = false;

    protected void FixedUpdate(){

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
    public void FIRE(){
        CanShoot = true;
    }
    public void STOPFIRE(){
        CanShoot = false;
    }
    
    public void Danger(){
    float distanceToPlayer = Vector3.Distance(playerTransform.position, startingPosition);

    if (distanceToPlayer < chaseLength)
    {
        if (distanceToPlayer < triggerLength)
        {
            chasing = true;
        }

        if (chasing)
        {
            // Set speeds for running away
            xSpeed = chaseXSpeed;
            ySpeed = chaseYSpeed;

            if (!collidingWithPlayer)
            {
                // Run away from the player
                Vector3 directionAwayFromPlayer = (transform.position - playerTransform.position).normalized;
                UpdateMotor(directionAwayFromPlayer);
                chasing = false;
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
        Debug.Log("not chasing");
        UpdateMotor(startingPosition - transform.position);

        // Reset to default speeds
        xSpeed = defaultXSpeed;
        ySpeed = defaultYSpeed;

        // Stop walking animation
        SetAnimationState(false);
    }


    }

    public void shoot(){
        if (Time.time - lastTimeFired > cooldown)
        {
            if(CanShoot){
                anim.SetTrigger("Shoot");
                lastTimeFired = Time.time;
                GameObject projectileThrow = Instantiate(projectile, firePoint.position, firePoint.rotation);
                CanShoot = false;
            }
        }
    
    }
}
