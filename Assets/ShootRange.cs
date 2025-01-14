using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRange : Collidable
{
    public GameObject enemyGameObject;  // Reference to the enemy's GameObject
    public GameObject colliderSource;   // Reference to the object providing the collider
    private bool Detected = false;

    protected override void Start()
    {
        // Assign boxCollider from the specified colliderSource GameObject
        if (colliderSource != null)
        {
            boxCollider = colliderSource.GetComponent<BoxCollider2D>();
            if (boxCollider == null)
            {
                Debug.LogError($"No BoxCollider2D found on {colliderSource.name}. Please attach one.");
            }
        }
        else
        {
            Debug.LogError("Collider Source not assigned in ShootRange.");
        }

        base.Start(); // Call base Start to configure the ContactFilter2D
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "PLAYER" && !Detected)
        {
            Detected = true;
            FIRE();
            Debug.Log("fire");
        }
    }

    public void FIRE()
    {
        if (Detected && enemyGameObject != null)
        {
            demonShooter demonShooter = enemyGameObject.GetComponent<demonShooter>();
            if (demonShooter != null)
            {
                demonShooter.FIRE();
            }
            else
            {
                Debug.LogWarning("demonShooter component not found on enemyGameObject.");
            }
        }
    }
}
