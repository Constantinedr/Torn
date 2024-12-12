using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;


    private BoxCollider2D weaponCollider; // Renamed field to avoid conflict

    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        weaponCollider = GetComponent<BoxCollider2D>();
        if (weaponCollider == null)
        {
            Debug.LogError($"BoxCollider2D is missing on {gameObject.name}. Please attach one.");
        }
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0)) // Left mouse button
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }
   
    private void Swing()
    {
        Debug.Log("Swing");
    }
}
