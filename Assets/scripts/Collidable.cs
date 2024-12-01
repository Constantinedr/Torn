using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter; // Filter for specific layers
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        // Configure the ContactFilter2D
        filter.SetLayerMask(LayerMask.GetMask("actor")); // Replace "TargetLayer" with your desired layer
        filter.useLayerMask = true;
    }

    protected virtual void Update()
    {
        // Detect collisions
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            // Reset the hit after processing
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log($"Collided with: {coll.name}");
    }
}
