using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isActivated = false; // Flag to check if the checkpoint is already activated

    // This method is called when something enters the checkpoint trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that collided with the checkpoint is the ball
        if (other.CompareTag("Ball") && !isActivated)
        {
            // If the ball enters, save the position (this could also be set to PlayerProgress or another system)
            PlayerProgress.Instance.SaveCheckpoint(transform.position);

            // Mark this checkpoint as activated
            isActivated = true;
            Debug.Log("Checkpoint Reached!");
        }
    }
}

