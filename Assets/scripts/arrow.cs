using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Collidable
{
    public int damagePoint = 1;
    public float pushForce = 2.0f;
    private Animator anim;


    public float lifeTime = 5f; // Time before the arrow despawns if not hitting anything

    protected override void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("Shoot");
        // Destroy the arrow after a certain time
        Destroy(gameObject, lifeTime);
    }

    protected override void OnCollide(Collider2D coll)
    {
        // Ignore collision with the player
        if (coll.CompareTag("FIGHTER"))
        {
            if (coll.name == "PLAYER"){
             

            // Destroy the arrow on impact
                Destroy(gameObject);
                Debug.Log("hit");
            // Apply damage to the target
                Damage dmg = new Damage
                {
                    damageAmount = damagePoint,
                    origin = transform.position,
                    pushForce = pushForce
                };

                coll.SendMessage("ReceiveDamage", dmg, SendMessageOptions.DontRequireReceiver);
                }
        }
    }
}
