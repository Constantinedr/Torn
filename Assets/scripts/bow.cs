using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    private BoxCollider2D weaponCollider; // Reference to the weapon's collider
    private Animator anim;
    private bool isPulling = false; // Tracks if the bow is being pulled

    private bool space = false;

    // References to the arrow prefab and the firing point
    public GameObject arrowPrefab; // Arrow prefab to instantiate
    public Transform firePoint; // Position where the arrow is instantiated

    // Force for the arrow shot
    public float arrowForce = 3f;

    // Variables for holding time tracking
    private float pullStartTime = 0f; // When the right mouse button was pressed
    private float pullThreshold = 0.5f; // Time needed to hold before firing

    protected void Start()
    {
        weaponCollider = GetComponent<BoxCollider2D>();
        if (weaponCollider == null)
        {
            Debug.LogError($"BoxCollider2D is missing on {gameObject.name}. Please attach one.");
        }

        // Initially disable the collider
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError($"SpriteRenderer is missing on {gameObject.name}. Please attach one.");
        }

        anim = GetComponent<Animator>();
    }

    protected void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            space = true;
        }
        else
        {
            space = false;
        }

        // Start pulling when right mouse button is held down
        if (Input.GetMouseButtonDown(1) && space == false) // Right mouse button pressed
        {
            StartPull();
        }

        if (Input.GetMouseButtonUp(1)) // Right mouse button released
        {
            ReleaseBow();
        }
    }

    private void StartPull()
    {
        isPulling = true;
        pullStartTime = Time.time; // Record the time the button was pressed

        // Play pull animation
        anim.SetTrigger("pull");

        // Enable the sprite and collider
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
        }
    }

    private void ReleaseBow()
    {
        isPulling = false;

        // Check if the button was held long enough
        float holdDuration = Time.time - pullStartTime;
        if (holdDuration >= pullThreshold)
        {
            ShootArrow(); // Instantiate and shoot the arrow
        }
        else
        {
            Debug.Log("Bow release too early, no arrow shot.");
        }

        // Play release animation
        anim.SetTrigger("release");

        // Disable the collider after release
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
    }

    private void ShootArrow()
    {
        if (arrowPrefab != null && firePoint != null)
        {
            // Instantiate the arrow at the firePoint
            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);

            // Get the Rigidbody2D component of the arrow
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Add force to the arrow in the forward direction
                rb.AddForce(firePoint.right * arrowForce, ForceMode2D.Impulse);
            }
        }
    }
}
