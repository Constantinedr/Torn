using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeaderboardCreatorDemo;
public class Player : Mover
{
    public int level;
    public GameObject LeaderboardManager2;
    private bool IsMoving;
    private bool DefyDeathBool = false;
    private int HellHoundFuryBoolBuff = 0;
    public GameObject DeathMenu;
    public GameObject weapon;
    private Animator anim;
    private bool isAlive = true;
    private HeartManager heartManager;
    private float speedBuffMultiplier = 1.2f;
    private float speedReductionMultiplier = 0.4f; // Reduction multiplier for slower movement
    private bool isSpeedBuffActive = false;
    private bool isSpeedReductionActive = false;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float damageMultiplier = 0;
    public bool HellHoundFuryBool = false;
    private float dashTime;

    private float cooldownTime;
    private bool isDashing;
    private Vector2 dashDirection;
    private int HellhoundActive = 0;
    private Rigidbody2D rb;
    
    private int lastHitpoint; // Store previous HP for change detection

    public void DestroyObjcect()
    {
        Destroy(gameObject);
    }

    public void Freeze()
    {
        // Stop all movement and actions
        isAlive = false; // Prevent FixedUpdate from handling movement
        anim.enabled = false; // Stop animations
        this.enabled = false; // Disable the Player script
    }

    public void Unfreeze()
    {
        // Restore all movement and actions
        isAlive = true;
        anim.enabled = true;
        this.enabled = true;
    }

    public void HeartSteel(int x)
    {
        maxHitpoint += x;
        heartManager?.InitializeHearts(maxHitpoint);
        heartManager?.UpdateHearts(hitpoint);
    }

    public void ActivateSpeedBuffPASSIVE()
    {
        if (!isSpeedBuffActive)
        {
            xSpeed *= speedBuffMultiplier;
            ySpeed *= speedBuffMultiplier;
        }
    }

    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    private void Update()
    {
        HandleDashInput();
        HandleAbilities();
        HandleMouseMovement();
    }

    private void HandleMouseMovement()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) // Left or Right mouse button
        {
            FaceMouse();
        }
    }

    private void FaceMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the z-coordinate is zero for 2D space

        if (mousePosition.x > transform.position.x)
        {
            // Face right
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Face left
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }

    protected override void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        if (moveDelta.x != 0)
        {
            // Face direction of movement
            HandleFacingDirection(moveDelta.x);
        }

        // Add push force and reduce gradually
        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverSpeed);

        // Apply movement and handle collisions
        HandleCollisionAndMovement();
    }

    private void HandleFacingDirection(float xMovement)
    {
        if (xMovement > 0)
        {
            // Face right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (xMovement < 0)
        {
            // Face left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void HandleCollisionAndMovement()
    {
        // Vertical movement
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("actor", "blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        // Horizontal movement
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("actor", "blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    private void HandleAbilities()
    {
        if (HellHoundFuryBool)
        {
            if (hitpoint <= maxHitpoint * 0.25f && HellhoundActive == 0)
            {
                damageMultiplier += HellHoundFuryBoolBuff * 2;
                HellhoundActive = 1;
            }
            else if (hitpoint > maxHitpoint * 0.25f && HellhoundActive == 1)
            {
                damageMultiplier = 0;
                HellhoundActive = 0;
            }
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

        if (isAlive)
        {
            heartManager?.UpdateHearts(hitpoint);
            HandleMovement();

            if (isDashing)
            {
                PerformDash();
            }
        }
    }

    private void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= cooldownTime)
        {
            dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            if (dashDirection != Vector2.zero)
            {
                StartDash();
            }
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTime = Time.time + dashDuration;
        cooldownTime = Time.time + dashCooldown;
    }

    private void PerformDash()
    {
        rb.velocity = dashDirection * dashSpeed;

        if (Time.time >= dashTime)
        {
            isDashing = false;

            rb.velocity = new Vector2(0f, 0f); // Stop movement after dash
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
            IsMoving = true;
        }
        else
        {
            // Player is not moving
            anim.SetBool("IsWalking2", false); // Stop the walking animation
            IsMoving = false;
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

    public void HellHoundFury()
    {
        HellHoundFuryBool = true;
        HellHoundFuryBoolBuff += 1;
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (Time.time - LastImmune > immuneTime && !isDashing)
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

    public void DefyDeath()
    {
        DefyDeathBool = true;
    }

    protected override void Death(){
        if (DefyDeathBool){
            DefyDeathBool = false;
            hitpoint = 5;
            GameManager.instance.ShowText("RESSURECT!", 25, Color.yellow, transform.position, Vector3.up * 40, 1f);
        }
        else{
                isAlive = false;
                HellhoundActive=0;
                damageMultiplier = 0;
                maxHitpoint = 10;
                    if (isSpeedBuffActive)
            {
                xSpeed /= speedBuffMultiplier;
                ySpeed /= speedBuffMultiplier;
                isSpeedBuffActive = false;
            }
            if (isSpeedReductionActive)
            {
                xSpeed /= speedReductionMultiplier;
                ySpeed /= speedReductionMultiplier;
                isSpeedReductionActive = false;
            }
                hitpoint = maxHitpoint;
                heartManager?.InitializeHearts(maxHitpoint);
                heartManager?.UpdateHearts(hitpoint);
                Freeze();
                LeaderboardManager2 leaderboardManager = LeaderboardManager2.GetComponent<LeaderboardManager2>();

                    if (leaderboardManager != null)
                    {
                        leaderboardManager.UploadEntry();
                    }
                GameManager.instance.deathMenuAnim.SetTrigger("Show");
        }
    }
    

}

