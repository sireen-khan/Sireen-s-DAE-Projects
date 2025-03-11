using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public static PlayerProgress Instance;

    private Vector2 checkpointPosition; // Save the position of the last checkpoint

    void Awake()
    {
        // Ensure there's only one instance of PlayerProgress
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This will preserve this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate if there's already an instance
        }
    }

    // Save the checkpoint position
    public void SaveCheckpoint(Vector2 position)
    {
        checkpointPosition = position;
        Debug.Log("Checkpoint saved at: " + position);
    }

    // Load the saved checkpoint position (can be used for respawn)
    public Vector2 LoadCheckpoint()
    {
        return checkpointPosition;
    }
}

