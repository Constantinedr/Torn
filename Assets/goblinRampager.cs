using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinRampager : Enemy
{
    public float speedReductionMultiplier = 0.5f; // Multiplier for reduced speed
    private bool isSlowed = false;
    private bool isSpeedReductionActive = false;

   
    public void Freeze()
    {
    // Stop all movement and actions
    
    anim.enabled = false; // Stop animations
    this.enabled = false; // Disable the Player script
    }

    private void Update()
    {
        if (isSlowed && !isSpeedReductionActive)
        {
            ActivateSpeedReduction();
        }
        else if (!isSlowed && isSpeedReductionActive)
        {
            DeactivateSpeedReduction();
        }
    }

    public void SlowDown(bool shouldSlow)
    {
        isSlowed = shouldSlow;
    }

    private void ActivateSpeedReduction()
    {
        xSpeed *= speedReductionMultiplier;
        ySpeed *= speedReductionMultiplier;
        isSpeedReductionActive = true;
    }

    private void DeactivateSpeedReduction()
    {
        xSpeed /= speedReductionMultiplier;
        ySpeed /= speedReductionMultiplier;
        isSpeedReductionActive = false;
    }
}
