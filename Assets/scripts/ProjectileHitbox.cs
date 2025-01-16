using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitbox : Collidable
{   
   public int damage;
   public float pushForce;
   private GameObject par;
   protected override void Start()
    {
        base.Start(); 
        par = transform.parent.gameObject;

    }

    protected override void OnCollide(Collider2D coll){
        if (coll.tag == "FIGHTER" && coll.name == "PLAYER"){
               
            Damage dmg = new Damage{ 
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };
            coll.SendMessage("ReceiveDamage",dmg); 
            Destroy(par);
        }
        else if (coll.tag == "WALL"){
            Destroy(par);
        }

    }
}
