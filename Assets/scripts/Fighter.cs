using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitpoint;
    public int maxHitpoint;
    public float pushRecoverSpeed;


    public float immuneTime = 0.2f;
    protected float LastImmune;

    protected Vector3 pushDirection;


    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - LastImmune > immuneTime)
        {
            LastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.white, transform.position,Vector3.up*10, 0.5f);

            if (hitpoint <=0){
                hitpoint = 0;
                Death ();
            }
    }
}    
    protected virtual void Death(){
       

    }
}
