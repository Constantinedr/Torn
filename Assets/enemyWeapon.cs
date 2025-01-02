using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWeapon : Collidable
{
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D weaponCollider;
    public float cooldown = 0.5f;
    private float lastSwing;
    private Animator anim;

    protected override void Start()
    {
        base.Start();
        weaponCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

  
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

    private void Swing()
    {
        if (anim != null)
        {
            anim.SetTrigger("Swing");
        }
        else
        {
            Debug.LogWarning("Animator not found on the knife object!");
        }
    }

    public void TriggerSwing()
    {
        // Expose Swing functionality for external triggers
        if (Time.time - lastSwing > cooldown)
        {
            lastSwing = Time.time;
            Swing();
        }
    }
}
