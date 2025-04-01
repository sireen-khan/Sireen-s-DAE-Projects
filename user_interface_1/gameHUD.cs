using UnityEngine;
using UnityEngine.UI;  // Required to modify UI elements

public class GameHUD : MonoBehaviour
{
    public Text scoreText;
    public Text frameText;

    private int score = 0;
    private int currentFrame = 1;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);  // Set initial score to 0
        UpdateFrame(1);  // Set initial frame to 1
    }

    // Method to update the score text
    public void UpdateScore(int points)
    {
        score += points;  // Add points to the score
        scoreText.text = "Score: " + score.ToString();  // Update the score UI element
    }

    // Method to update the frame text
    public void UpdateFrame(int frame)
    {
        currentFrame = frame;
        frameText.text = "Frame: " + currentFrame.ToString();  // Update the frame UI element
    }

    // You can call this method from other scripts to update score and frame
    // Example usage:
    // gameHUD.UpdateScore(10);
    // gameHUD.UpdateFrame(2);
}
