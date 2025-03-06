using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject sparkleEffectPrefab; // Prefab for the sparkle effect
    public float sizeIncreaseFactor = 1.5f; // Factor by which the ball will increase in size

    // This is called when the ball (with the "Ball" tag) collides with the power-up.
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that collided with the power-up has the "Ball" tag
        if (other.CompareTag("Ball"))
        {
            // Get the BallControl component to increase the ball's size
            BallControl ballControl = other.GetComponent<BallControl>();
            if (ballControl != null)
            {
                ballControl.IncreaseSize(sizeIncreaseFactor); // Increase ball size
            }

            // Instantiate the sparkle effect at the ball's position
            if (sparkleEffectPrefab != null)
            {
                Instantiate(sparkleEffectPrefab, other.transform.position, Quaternion.identity);
            }

            // Destroy the power-up after it's collected
            Destroy(gameObject);
        }
    }
}
