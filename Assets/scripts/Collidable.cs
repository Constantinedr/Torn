using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter; // Filter for specific layers
    protected BoxCollider2D boxCollider; // Made protected for inheritance
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogError($"BoxCollider2D is missing on {gameObject.name}. Please attach one.");
        }

        // Configure the ContactFilter2D
        filter.SetLayerMask(LayerMask.GetMask("actor")); // Replace "actor" with your desired layer
        filter.useLayerMask = true;
    }

    protected virtual void Update()
    {
        if (boxCollider == null) return; // Safety check

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
