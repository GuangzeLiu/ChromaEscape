using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    public GameObject spikePrefab;  // Reference to the spike prefab
    public Transform spawnPoint;    // The location where the spikes will spawn
    public float spawnInterval = 5f;  // Time interval between spike spawns

    private float timer;  // Internal timer to track intervals

    void Start()
    {
        // Spawn an initial spike immediately when the game starts
        SpawnSpike();
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if the timer exceeds the spawn interval
        if (timer >= spawnInterval)
        {
            SpawnSpike();  // Spawn a new spike
            timer = 0f;    // Reset the timer
        }
    }

    // Method to instantiate the spike
    void SpawnSpike()
    {
        Instantiate(spikePrefab, spawnPoint.position, Quaternion.identity);
    }
}
