using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mimicChest : Collectable
{
    private Animator anim;
    public GameObject replacementPrefab; // The prefab to spawn after destruction

    protected override void Start()
    {
        base.Start(); // Ensure base class initialization
        anim = GetComponent<Animator>();
    }

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            anim.SetTrigger("mimic");
            GameManager.instance.ShowText("+" + " gold!", 25, Color.yellow, transform.position, Vector3.up * 50, 3.0f);
            StartCoroutine(ActivateAndReplace());
        }
    }

    private IEnumerator ActivateAndReplace()
    {
        yield return new WaitForSeconds(1f); // Wait for 0.5 seconds

        // Spawn the replacement prefab at the same position and rotation
        if (replacementPrefab != null)
        {
            Instantiate(replacementPrefab, transform.position, transform.rotation);
        }

        // Destroy the current object
        Destroy(gameObject);
    }
}
