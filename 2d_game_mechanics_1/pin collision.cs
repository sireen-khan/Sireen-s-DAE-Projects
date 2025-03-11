using UnityEngine;

public class Pin : MonoBehaviour
{
    // Variable to control if the pin is already destroyed
    private bool isDestroyed = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object that collided with the pin has the "Ball" tag
        if (!isDestroyed && collision.gameObject.CompareTag("Ball"))
        {
            // Set the pin to be destroyed after a short delay
            isDestroyed = true;

            // Option 1: Destroy the pin immediately
            // Destroy(gameObject);

            // Option 2: Apply force to simulate the pin falling
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false; // Enable physics for the pin to fall
                rb.AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(5f, 10f)), ForceMode2D.Impulse); // Random force for a natural fall
            }
            Destroy(gameObject, 2f); // Destroy the pin after 2 seconds

        }
    }
}
