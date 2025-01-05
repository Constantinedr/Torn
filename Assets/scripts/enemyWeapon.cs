using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWeapon : Collidable
{
    public int damagePoint = 1;
    public float pushForce = 2.0f;
    public GameObject character; // Reference to the character with the Freeze component
    private BoxCollider2D weaponCollider;
    public float cooldown = 0.5f;
    private float lastSwing;
    private Animator anim;

    private goblinRampager goblin; // Reference to the goblinRampager

    protected override void Start()
    {
        base.Start();
        weaponCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        // Find the goblinRampager component in the parent object
        goblin = GetComponentInParent<goblinRampager>();
        if (goblin == null)
        {
            Debug.LogWarning("goblinRampager not found on parent object!");
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    private void Swing()
    {
        if (anim != null)
        {
            anim.SetTrigger("Swing");

            // Slow down the goblin during the swing
            if (goblin != null)
            {
                goblin.SlowDown(true);
            }

            // Trigger Freeze if character has the goblinRampager component
            if (character != null)
            {
                var freezeScript = character.GetComponent<goblinRampager>();
                if (freezeScript != null)
                {
                    freezeScript.Freeze();
                }
            }

            // Restore speed after cooldown
            Invoke("RestoreSpeed", cooldown);
        }
        else
        {
            Debug.LogWarning("Animator not found on the weapon object!");
        }
    }

    private void RestoreSpeed()
    {
        if (goblin != null)
        {
            var freezeScript = character.GetComponent<goblinRampager>();
            freezeScript.UnFreeze();
        }
        
    }

    public void TriggerSwing()
    {
        if (Time.time - lastSwing > cooldown)
        {
            lastSwing = Time.time;
            Swing();
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("FIGHTER"))
        {
            if (coll.name == "PLAYER")
            {
                Damage dmg = new Damage
                {
                    damageAmount = damagePoint,
                    origin = transform.position,
                    pushForce = pushForce
                };

                coll.SendMessage("ReceiveDamage", dmg);
            }
        }
    }
}
