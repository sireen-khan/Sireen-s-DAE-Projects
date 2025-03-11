using UnityEngine;
using UnityEngine.UI; // For UI Text

public class BowlingGameManager : MonoBehaviour
{
    // References to the ball and pin prefabs, spawn point, and pin reset position
    public GameObject ballPrefab; // The bowling ball prefab
    public Transform spawnPoint; // The point where the ball spawns at the start of each turn
    public GameObject pinsParent; // Parent object holding all the pins
    public GameObject pinPrefab; // Prefab for individual pins

    // Reference to the Pin Reset Position Transform
    public Transform pinResetPosition;  // This will allow you to drag the PinResetPosition GameObject in the Inspector

    // UI Elements
    public Text messageText; // To show the "Well Done" message

    private GameObject currentBall; // To keep track of the current ball
    private bool isDragging = false; // To check if the ball is being dragged
    private bool isRolling = false; // To track if the ball is rolling
    private Vector3 offset; // To store the offset between mouse and ball position
    private float rollTime = 5f; // Time for how long the ball rolls (in seconds)
    private bool gameIsOver = false; // To track if the game is over
    private int rollsThisTurn = 0; // To track the number of rolls during the turn

    void Start()
    {
        messageText.gameObject.SetActive(false); // Hide the message text initially
        StartNewTurn(); // Start the first turn when the game starts
    }

    void Update()
    {
        if (gameIsOver) return; // Don't process if the game is over.

        // Detect input for dragging the ball (mouse or touch)
        if (Input.GetMouseButtonDown(0) && !isRolling)
        {
            // Check if the mouse clicks on the ball
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == currentBall)
                {
                    isDragging = true;
                    // Calculate the offset between the mouse position and the ball
                    offset = currentBall.transform.position - hit.point;
                }
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            // Move the ball based on mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                currentBall.transform.position = hit.point + offset;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            // Stop dragging the ball
            isDragging = false;
            // When the drag stops, apply a force to make the ball roll in the direction it was moved
            StartBallRoll();
        }
    }

    // Function to start a new turn
    void StartNewTurn()
    {
        rollsThisTurn = 0; // Reset the roll count for each turn
        gameIsOver = false; // Reset the game over status
        SpawnBall();
        ResetPins();
    }

    // Function to spawn a new ball at the beginning of the player's turn
    void SpawnBall()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("Ball Prefab is not assigned!");
            return;
        }

        if (currentBall != null)
        {
            Destroy(currentBall); // Destroy the old ball if it exists
        }

        // Instantiate the new ball at the spawn point
        currentBall = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);

        // Ensure the ball doesn't move automatically when spawned
        Rigidbody ballRigidbody = currentBall.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            ballRigidbody.linearVelocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
        }

        // Log to check if the ball is being instantiated
        Debug.Log("Spawning new ball...");
    }

    // Function to reset pins back to their starting position
    void ResetPins()
    {
        // Destroy all the existing pins first
        foreach (Transform pin in pinsParent.transform)
        {
            Destroy(pin.gameObject); // Destroy the pin GameObject
        }

        // Instantiate new pins at their default positions
        for (int i = 0; i < 10; i++) // Assuming 10 pins in bowling
        {
            Vector3 pinPosition = pinResetPosition.position + new Vector3(i * 0.5f, 0, 0); // Adjusted for a linear arrangement
            GameObject pin = Instantiate(pinPrefab, pinPosition, Quaternion.identity);
            pin.transform.SetParent(pinsParent.transform); // Parent the pin to the pinsParent GameObject
        }
    }

    // Function that is called when a player rolls the ball
    public void RollBall()
    {
        rollsThisTurn++; // Increment the roll count for each roll
        isRolling = true; // Mark the ball as rolling

        // Get the ball's Rigidbody component
        Rigidbody ballRigidbody = currentBall.GetComponent<Rigidbody>();

        if (ballRigidbody != null)
        {
            // Give the ball some initial force to start rolling
            ballRigidbody.AddForce(spawnPoint.forward * 10f, ForceMode.VelocityChange);
        }

        // Call StopRolling() after a set time (e.g., 5 seconds)
        Invoke("StopRolling", rollTime); // Stop the ball after 'rollTime' seconds
    }

    // Function to stop the ball after the set time
    void StopRolling()
    {
        // Stop the ball's movement
        if (currentBall != null)
        {
            Rigidbody ballRigidbody = currentBall.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.linearVelocity = Vector3.zero; // Stop the ball from moving
                ballRigidbody.angularVelocity = Vector3.zero; // Stop the ball from spinning
            }
        }

        // Show the "Well Done" message
        ShowMessage("Well Done!");

        // End the game and reset everything
        gameIsOver = true;
        Invoke("StartNewTurn", 2f); // Reset the game after a short delay (2 seconds)
    }

    // Function to show a message on the screen
    void ShowMessage(string message)
    {
        messageText.gameObject.SetActive(true); // Show the message
        messageText.text = message; // Set the message text
    }

    // Function to start the ball rolling after it is dragged and released
    private void StartBallRoll()
    {
        if (currentBall != null)
        {
            Rigidbody ballRigidbody = currentBall.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                // Apply a force based on the direction of the drag
                Vector3 direction = currentBall.transform.position - spawnPoint.position; // Calculate direction
                ballRigidbody.AddForce(direction.normalized * 10f, ForceMode.VelocityChange);
            }
        }
    }
}

