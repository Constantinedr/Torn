using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyweaposmall : enemyWeapon
{

    
    private SpriteRenderer spriteRendererffd;

    protected override void Start()
    {
        spriteRendererffd =  GetComponent<SpriteRenderer>();
        base.Start();

    }

protected override void Swing()
    {
            StartCoroutine(FlashBeforeDash());
            Invoke(nameof(swingsword), 0.2f);
           

    }
    private void swingsword() {

        anim.SetTrigger("Swing");

    }

    private IEnumerator FlashBeforeDash()

    {
       
        
            spriteRendererffd.color = Color.yellow;
            yield return new WaitForSeconds(0.4f);
            spriteRendererffd.color = Color.white;

        
    }
}
