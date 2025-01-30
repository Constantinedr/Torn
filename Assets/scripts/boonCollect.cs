using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boonCollect : Collectable
{
    public GameObject boonOptions; 
    protected override void Start (){

        base.Start(); 
        boonOptions = GameObject.Find("boon0ptions");
    }

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            Animator animator = boonOptions.GetComponent<Animator>();
            BoonOptionsManager boonOptionsManager = boonOptions.GetComponent<BoonOptionsManager>();
            if (boonOptionsManager != null)
            {
                boonOptionsManager.reAssign();  // Shuffle the boons
            }
            animator.SetTrigger("ShowBOON");

            Destroy(gameObject);
        }
    }
}
