using UnityEngine;

public class BowlingGameLogic : MonoBehaviour
{
    public GameHUD gameHUD;  // Reference to GameHUD script to update UI
    private int[] pinsKnockedDownPerRoll = new int[21];  // Maximum rolls in a bowling game
    private int currentRoll = 0;
    private int currentFrame = 1;
    private int score = 0;
    private int currentTurn = 1;  // Track the current turn

    // Call this method when the player bowls a roll (pass in the number of pins knocked down)
    public void Bowl(int pins)
    {
        // Update the pins knocked down for this roll
        pinsKnockedDownPerRoll[currentRoll] = pins;
        currentRoll++;

        // Update the score and frame after the roll
        UpdateScoreAndFrame();

        // Update the UI
        gameHUD.UpdateScore(score);
        gameHUD.UpdateFrame(currentFrame);
    }

    // Example function that gets called when a player completes a round or a frame
    public void OnPlayerBowls(int points)
    {
        // Update the score
        gameHUD.UpdateScore(points);

        // If the player finished a frame (e.g., every 10 turns, move to next frame)
        if (currentTurn == 10)  // Assuming 10 frames
        {
            gameHUD.UpdateFrame(currentFrame + 1);  // Go to next frame
        }
    }

    // Method to update score and frame based on the game rules
    void UpdateScoreAndFrame()
    {
        if (currentFrame <= 10)  // There are 10 frames in total
        {
            // Handle strikes (10 pins in one roll)
            if (pinsKnockedDownPerRoll[currentRoll - 1] == 10 && currentRoll % 2 == 1)  // Strike on first roll of the frame
            {
                score += 10 + pinsKnockedDownPerRoll[currentRoll] + pinsKnockedDownPerRoll[currentRoll + 1];
                currentFrame++;  // Move to the next frame after a strike
            }
            // Handle spares (10 pins in two rolls)
            else if (currentRoll % 2 == 0 && pinsKnockedDownPerRoll[currentRoll - 1] + pinsKnockedDownPerRoll[currentRoll - 2] == 10)
            {
                score += 10 + pinsKnockedDownPerRoll[currentRoll];
                currentFrame++;  // Move to the next frame after a spare
            }
            // Handle open frames
            else if (currentRoll % 2 == 0)
            {
                score += pinsKnockedDownPerRoll[currentRoll - 1] + pinsKnockedDownPerRoll[currentRoll - 2];
                currentFrame++;  // Move to the next frame after an open frame
            }
        }
    }
}

