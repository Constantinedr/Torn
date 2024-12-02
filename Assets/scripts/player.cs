using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    // Dash-related variables
    public float dashDistance = 0.5f;   // Maximum dash distance
    public float dashDuration = 0.2f; // Duration of the dash
    public float dashCooldown = 0.2f;   // Time before you can dash again

    private float dashTime;           // Timer for the dash duration
    private float cooldownTimer;      // Timer for the cooldown
    private bool isDashing = false;   // Whether the player is currently dashing
    private Vector2 dashDirection;    // Direction of the dash
    private Vector2 originalPosition; // Starting position for the dash
    private Vector2 targetPosition;   // Target position for the dash

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            DashMove();
            return; // Skip normal movement when dashing
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDelta = new Vector3(x, y, 0);

        if (moveDelta.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("actor", "blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("actor", "blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

        HandleDashInput(x, y);
    }

    private void HandleDashInput(float x, float y)
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            return; // Don't process dash input if on cooldown
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            StartDash(x, y);
        }
    }


       private void StartDash(float x, float y)
{
    dashDirection = new Vector2(x, y).normalized;

    if (dashDirection == Vector2.zero)
    {
        dashDirection = Vector2.right;
    }

    // Perform a boxcast to check for obstacles
    RaycastHit2D hit = Physics2D.BoxCast(
        transform.position,
        boxCollider.size,
        0f,
        dashDirection,
        dashDistance,
        LayerMask.GetMask("actor", "blocking")
    );

    // If an obstacle is detected, don't start the dash
    if (hit.collider != null)
    {
        return;
    }

    // Start the dash if no obstacles are detected
    isDashing = true;
    dashTime = dashDuration;
    cooldownTimer = dashCooldown;
    originalPosition = transform.position;
    targetPosition = originalPosition + dashDirection * dashDistance;
}

    

    private void DashMove()
    {
        // Interpolate towards the target position over the duration of the dash
        transform.position = Vector2.Lerp(originalPosition, targetPosition, 1 - (dashTime / dashDuration));
        dashTime -= Time.deltaTime;

        if (dashTime <= 0)
        {
            EndDash();
        }
    }

    private void EndDash()
    {
        isDashing = false;
        transform.position = targetPosition;
    }
}
