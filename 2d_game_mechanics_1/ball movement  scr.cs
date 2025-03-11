using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float throwForce = 10f;   // Power of the throw
    public Vector2 initialDirection; // Direction the ball is thrown in

    public float linearDrag = 5f; // Increase this value to slow the ball over time

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detect player input (for example, space bar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Apply force to the ball when space is pressed
            ThrowBall();
        }
    }

    void ThrowBall()
    {
        // Add force to move the ball forward
        rb.AddForce(initialDirection * throwForce, ForceMode2D.Impulse);
    }

   
}

