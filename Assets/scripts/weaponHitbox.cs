using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponHitbox : Collidable
{
    public GameObject knife;

    protected override void OnCollide(Collider2D coll)
    {

        if (coll.CompareTag("FIGHTER"))
        {
            if (coll.name == "PLAYER"){
                
                if (knife != null)
                {
            // Attempt to get the enemyWeapon script from the knife GameObject
                    var enemyWeaponScript = knife.GetComponent<enemyWeapon>();

                    if (enemyWeaponScript != null)
                    {
                // Trigger the Swing functionality in the enemyWeapon script
                        enemyWeaponScript.TriggerSwing();
                        Debug.Log("Swing triggered on knife.");
                    }
                    else
                    {
                     Debug.LogWarning("enemyWeapon script not found on knife.");
                    }
                }
                else
                {
                    Debug.LogError("Knife GameObject is not assigned.");
                }
        }
    }
}
}
