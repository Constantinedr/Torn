using System.Collections;
using UnityEngine;

public class SPAWNER : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject ENEMYPREFAB;   // Prefab for normal activation
    public GameObject ENEMYPREFAB2; // Prefab for calm activation
    public float rotationSpeed = 10f; // Speed of circular movement
    private Vector3 spawnPosition;

    void Awake()
    {
        ENEMYPREFAB = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        ENEMYPREFAB2 = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        spawnPosition = transform.position;
    }



    public void TriggerActivate()
    {
        StartCoroutine(Activate());
    }
    public void TriggerCalmActivate()
    {
        StartCoroutine(CalmActivate());
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(1f); 

        if (ENEMYPREFAB != null)
        {
            Instantiate(ENEMYPREFAB, transform.position, transform.rotation);
        }
    }

    private IEnumerator CalmActivate()
    {
        yield return new WaitForSeconds(1f); 

        if (ENEMYPREFAB2 != null)
        {
            Instantiate(ENEMYPREFAB2, transform.position, transform.rotation);
        }
    }
}
