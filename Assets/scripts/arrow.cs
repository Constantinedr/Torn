using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : Collidable
{
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    public int weaponLevel = 0;
    private Rigidbody2D rb;
    public float lifeTime = 5f; // Time before the arrow despawns if not hitting anything

    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Destroy the arrow after a certain time
        Destroy(gameObject, lifeTime);
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
}
