using UnityEngine;

public class Pin : MonoBehaviour
{
    private bool isKnockedDown = false; // Flag to check if the pin has been knocked down
    private Quaternion initialRotation; // To store the pin's initial rotation

    // Add this if you want to respawn the pin after it has been knocked down
    public PinSpawner pinSpawner; // Reference to PinSpawner to respawn pins

    void Start()
    {
        initialRotation = transform.rotation; // Store the initial rotation of the pin
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the pin is hit by the bowling ball
        if (collision.gameObject.CompareTag("BowlingBall"))
        {
            // Mark the pin as potentially knocked down
            CheckIfKnockedDown();
        }
    }

    // Function to check if the pin is knocked down
    void CheckIfKnockedDown()
    {
        // If the pin's rotation angle is too high (indicating it has fallen)
        if (Mathf.Abs(transform.rotation.eulerAngles.x) > 45f || Mathf.Abs(transform.rotation.eulerAngles.z) > 45f)
        {
            if (!isKnockedDown) // Only mark as knocked down once
            {
                isKnockedDown = true;
                // Optionally, you can deactivate the pin when it falls
                gameObject.SetActive(false);

                // Tell the PinSpawner to respawn the pin (if you want to respawn automatically)
                pinSpawner?.RespawnPin(transform.GetSiblingIndex());
            }
        }
    }

    // Optional function to reset the pin if you want to check every frame
    void Update()
    {
        // Check if the pin's rotation angle is restored and reactivate it
        if (isKnockedDown && Mathf.Abs(transform.rotation.eulerAngles.x) < 10f && Mathf.Abs(transform.rotation.eulerAngles.z) < 10f)
        {
            gameObject.SetActive(true); // Reactivate the pin when it's upright again
            isKnockedDown = false;
        }
    }
}

