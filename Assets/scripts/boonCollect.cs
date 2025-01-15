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
            Animator animator = boonOptions.GetComponent<Animator>();
            animator.SetTrigger("Show");

            Destroy(gameObject);
        }
    }
}
