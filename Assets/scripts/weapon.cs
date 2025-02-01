using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : Collidable
{

    private AudioSource WeaponSwing;
    public int damagePoint = 1;
    public float pushForce = 2.0f;
    public List<Sprite> weaponSprites;
    private int currentSpriteIndex = 0;


    public int weaponLevel = 1;
    private SpriteRenderer spriteRenderer;

    private BoxCollider2D weaponCollider; // Reference to the weapon's collider

    public float cooldown = 0.2f;
    private float lastSwing;
    private Animator anim;
    public void NextSprite()
    {
        damagePoint += 1;
        currentSpriteIndex = (currentSpriteIndex + 1) % weaponSprites.Count;
        spriteRenderer.sprite = weaponSprites[currentSpriteIndex];
    }

        public void Freeze()
    {
    
        anim.enabled = false; // Stop animations
        this.enabled = false; // Disable the Player script
    }

    public void Unfreeze()
    {   
        anim.enabled = true;
        this.enabled = true;
    }
    protected override void Start()
    {
        base.Start();
        WeaponSwing = GetComponent<AudioSource>();
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
           if (Input.GetMouseButton(1)) // Left mouse button
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                SwingHeavy();
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


    }
        private void SwingHeavy()
    {
        anim.SetTrigger("Heavy");

        // Enable the collider and sprite during the swing


    }

}