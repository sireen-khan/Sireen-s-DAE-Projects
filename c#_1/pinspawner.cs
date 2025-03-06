using UnityEngine;

public class PinSpawner : MonoBehaviour
{
    public GameObject pinPrefab;        // Pin prefab to spawn
    public float minSpawnInterval = 1f; // Minimum spawn interval in seconds
    public float maxSpawnInterval = 5f; // Maximum spawn interval in seconds

    void Start()
    {
        // Start spawning pins at random intervals
        InvokeRepeating("SpawnPin", 0f, Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    void SpawnPin()
    {
        // Generate a random X position within the specified range
        float randomX = Random.Range(-5f, 5f);  // Adjust the range as necessary for your lane width

        // Spawn the pin at the random X position, keeping the Y position the same as the spawner
        Instantiate(pinPrefab, new Vector2(randomX, transform.position.y), Quaternion.identity);
    }
}
