using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARROWSPAWNER : MonoBehaviour
{
    public GameObject arrowPrefab; // Reference to the arrow prefab
    public Transform spawnPoint; // The position where the arrows will spawn
    public float spawnInterval = 5f; // Time interval between spawns

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval; // Initialize the first spawn time
    }

    void Update()
    {
        // Check if it's time to spawn a new arrow
        if (Time.time >= nextSpawnTime)
        {
            SpawnArrow();
            nextSpawnTime = Time.time + spawnInterval; // Schedule the next spawn
        }
    }

    void SpawnArrow()
    {
        if (arrowPrefab != null && spawnPoint != null)
        {
            Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Arrow spawned!");
        }
        else
        {
            Debug.LogError("Arrow prefab or spawn point is not assigned!");
        }
    }
}
