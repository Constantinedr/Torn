using UnityEngine;

public class FASTDEMON : Enemy
{
    // Override the animation state method
    protected override void SetAnimationState(bool isWalking)
    {
        if (anim != null)
        {
            anim.SetBool("FASTDEMONRUN", isWalking);
        }
    }
}
