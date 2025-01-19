using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : Collectable
{
    public GameObject shopUI;
    private Animator anim;
   
    protected override void OnCollide(Collider2D coll){
    if (coll.name == "PLAYER")
         OnCollect();
        OpenShop();
            
    }
       protected override void Start()
    {
        base.Start();
        shopUI = GameObject.Find("ShopUI");
    }
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GameManager.instance.ShowText("Press F to epen Shop!",25,Color.white,transform.position,Vector3.up*50, 3.0f);

        }
    }
    private void OpenShop(){
        if (Input.GetKeyDown(KeyCode.F))
    {
    Debug.Log("hi");
    Animator shopAnimator = shopUI.GetComponent<Animator>();
    shopAnimator.SetTrigger("Show");
    ShopUi shopUIScript = shopUI.GetComponent<ShopUi>();
            if (shopUIScript != null)
            {
                shopUIScript.updateGold(); // Assuming the UpdateGold method exists in ShopUi script
            }
    }
    }
}
