using System.Collections;
using UnityEngine;

public class SPAWNER : MonoBehaviour
{
    public GameObject ENEMYPREFAB;   // Prefab for normal activation
    public GameObject ENEMYPREFAB2; // Prefab for calm activation

    // Public method to trigger the normal activation coroutine
    public void TriggerActivate()
    {
        StartCoroutine(Activate());
    }

    // Public method to trigger the calm activation coroutine
    public void TriggerCalmActivate()
    {
        StartCoroutine(CalmActivate());
    }

    // Coroutine to spawn ENEMYPREFAB after a delay
    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second

        if (ENEMYPREFAB != null)
        {
            Instantiate(ENEMYPREFAB, transform.position, transform.rotation);
        }
    }

    // Coroutine to spawn ENEMYPREFAB2 after a delay
    private IEnumerator CalmActivate()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second

        if (ENEMYPREFAB2 != null)
        {
            Instantiate(ENEMYPREFAB2, transform.position, transform.rotation);
        }
    }
}
