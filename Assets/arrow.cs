using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    public float lifeTime = 5f; // Time before the arrow despawns if not hitting anything

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Destroy the arrow after a certain time
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Add your collision logic here (e.g., apply damage to enemies)

    }
}
