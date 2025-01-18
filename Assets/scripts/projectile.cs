using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{   
    public GameObject player;
    public Transform target;   // The target the projectile will follow
    public float speed = 10f;  // Speed of the projectile
    public float lifetime = 5f; // Lifetime of the projectile

    private Vector3 direction;

    void Start()
    {   
        Destroy(gameObject, lifetime);
        player = GameObject.Find("PLAYER");
        target = player.transform;
        direction = (target.position - transform.position).normalized;
    }

    void Update()
    {

        transform.position += direction * speed * Time.deltaTime;

        // Optional: Rotate the projectile to face the target
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }
}
