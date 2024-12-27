using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    [SerializeField] private float speedBuffMultiplier = 1.5f; // How much faster the player moves during the buff
    private bool isSpeedBuffActive = false;

    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
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

  
    
}
