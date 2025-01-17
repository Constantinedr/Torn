using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest; // Fixed
            GameManager.instance.pesos += pesosAmount;
            GameManager.instance.ShowText("+" + pesosAmount + " gold!",25,Color.yellow,transform.position,Vector3.up*50, 3.0f);
        }
    }
}
