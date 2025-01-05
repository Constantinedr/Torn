using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinRampager : Enemy
{
    public float speedReductionMultiplier = 0.5f; // Multiplier for reduced speed
    private bool isSlowed = false;
    private bool isSpeedReductionActive = false;

    private float normalspeedx;
    private float normalspeedy;
    protected override void Start()
    {
        base.Start();
        normalspeedy = chaseYSpeed;
        normalspeedx = chaseXSpeed;
    }
    public void Freeze()
    {
        chaseXSpeed = normalspeedx;
        chaseYSpeed = normalspeedy;
        chaseXSpeed *= speedReductionMultiplier;
        chaseYSpeed *= speedReductionMultiplier;
        isSpeedReductionActive = true;
    }
    public void UnFreeze()
    {
        chaseXSpeed = normalspeedx;
        chaseYSpeed = normalspeedy;        
        
        isSpeedReductionActive = false;
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
