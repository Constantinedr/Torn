using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    private BoxCollider2D weaponCollider; // Reference to the weapon's collider

    private float cooldown = 0.3f;
    private float lastSwing;
    private Animator anim;

    protected override void Start()
    {
        base.Start();

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

    protected override void Update()
    {
        base.Update();

        // Trigger swing on left mouse button
        if (Input.GetMouseButton(0)) // Left mouse button
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("FIGHTER"))
        {
            if (coll.name == "PLAYER")
                return;

            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");

        // Enable the collider and sprite during the swing
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
        }
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

        // Disable the collider and sprite after a short duration
        Invoke(nameof(EndSwing), 0.3f); // Adjust the duration as needed
    }

    private void EndSwing()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }
}
