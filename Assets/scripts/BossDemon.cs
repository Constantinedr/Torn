using UnityEngine;

public class BossDemon : Enemy
{
    public SPAWNER[] spawners; // Array to hold references to the spawners
    private bool frenzyTriggered = false; // Ensure Frenzy triggers only once
    private bool calmTriggered = false;   // Ensure Calm triggers only once

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

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.up * 40, 1f);

            // Trigger Frenzy at 50% health
            if (!frenzyTriggered && hitpoint <= maxHitpoint * 0.5 && hitpoint > 0)
            {
                frenzy();
                frenzyTriggered = true;
            }

            // Trigger Calm at near max health
            if (!calmTriggered && hitpoint >= maxHitpoint - 1)
            {
                calm();
                calmTriggered = true;
            }

            // Handle Death
            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }

    protected virtual void frenzy()
    {
        if (spawners != null && spawners.Length > 0)
        {
            foreach (SPAWNER spawner in spawners)
            {
                spawner.TriggerActivate();
            }
        }
    }

    protected virtual void calm()
    {
        if (spawners != null && spawners.Length > 0)
        {
            foreach (SPAWNER spawner in spawners)
            {
                spawner.TriggerCalmActivate();
            }
        }
    }
}
