using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : Collectable
{
    public int pesosAmount = 1;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            Destroy(gameObject);
            GameManager.instance.pesos += pesosAmount;
            GameManager.instance.ShowText("+" + pesosAmount + " gold!",25,Color.yellow,transform.position,Vector3.up*50, 3.0f);
        }
    }
}
