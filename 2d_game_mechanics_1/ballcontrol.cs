using UnityEngine;

public class BallControl : MonoBehaviour
{
    
   public float throwForce = 5f;  // The power of the throw
    public float slowDownSpeed = 0.1f;  // Slower speed after ball is thrown
    
    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component to apply physics
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detect player input (for example, space bar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Throw the ball forward with a set force
            rb.AddForce(new Vector2(0, throwForce), ForceMode2D.Impulse);
        }
    }

    // Method to increase the ball's size when power-up is collected
    public void IncreaseSize(float sizeIncreaseFactor)
    {
        // Increase the size of the ball by modifying its transform scale
        transform.localScale *= sizeIncreaseFactor;
    }
}
