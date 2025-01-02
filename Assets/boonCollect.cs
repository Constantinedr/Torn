using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boonCollect : Collectable
{
    public GameObject boonOptions; 

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;

            // Trigger the animation on the boonOptions' Animator
            if (boonOptions != null)
            {
                Animator animator = boonOptions.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetTrigger("Show");
                }

                
            }

            // Destroy the collected item
            Destroy(gameObject);
        }
    }
}
