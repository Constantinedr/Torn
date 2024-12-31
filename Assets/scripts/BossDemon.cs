using UnityEngine;

public class BossDemon : Enemy
{
    // Override the animation state method
    protected override void SetAnimationState(bool isWalking)
    {
        if (anim != null)
        {
            anim.SetBool("BossDemonWalk", isWalking);
        }
    }
     protected override void ReceiveDamage(Damage dmg)
    {
        if (Time.time - LastImmune > immuneTime)
        {
            LastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position,Vector3.up*40, 1f);
            if (hitpoint >=maxHitpoint*0.5){
                frenzy();


            }
            if (hitpoint <=0){
                hitpoint = 0;
                Death ();
            }
    }
    }
    protected virtual void frenzy(){

    }
}    

