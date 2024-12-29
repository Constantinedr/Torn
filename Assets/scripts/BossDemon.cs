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
}
