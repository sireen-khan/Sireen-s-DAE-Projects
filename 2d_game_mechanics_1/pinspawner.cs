using UnityEngine;

public class PinSpawner : MonoBehaviour
{
    public GameObject pinPrefab;          // The Pin prefab to spawn
    public Transform[] pinSpawnPositions; // Array to hold the original positions where pins should spawn
    private GameObject[] pins;           // Array to hold the currently instantiated pins

    void Start()
    {
        pins = new GameObject[pinSpawnPositions.Length];
        SpawnPins(); // Spawn the initial pins at the start of the game
    }

    // Function to spawn the pins at their original positions
    void SpawnPins()
    {
        for (int i = 0; i < pinSpawnPositions.Length; i++)
        {
            // Instantiate each pin at its corresponding spawn position
            pins[i] = Instantiate(pinPrefab, pinSpawnPositions[i].position, pinSpawnPositions[i].rotation);
        }
    }

    // Function to respawn a specific pin (called when a pin is knocked down)
    public void RespawnPin(int pinIndex)
    {
        if (pins[pinIndex] == null) // If the pin is knocked down (destroyed), respawn it
        {
            // Instantiate the pin at its original position and rotation
            pins[pinIndex] = Instantiate(pinPrefab, pinSpawnPositions[pinIndex].position, pinSpawnPositions[pinIndex].rotation);
        }
    }

    // Function to check if a pin is knocked down (simplified version)
    // In this example, we're checking the pin's rotation. If the rotation goes beyond a threshold, we consider it knocked down.
    public void CheckPinStatus()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            if (pins[i] != null && pins[i].transform.rotation.eulerAngles.x > 45f)
            {
                // If the pin is knocked down, respawn it
                RespawnPin(i);
            }
        }
    }
}

