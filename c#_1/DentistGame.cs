using UnityEngine;

public class DentistGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public string patientName = "Timmy";

    // Private variable: Holds the status of the tooth (1 = rotten, 0 = healthy)
    private int toothStatus = 1; // Let's say 1 means the tooth is rotten

    // Start is called before the first frame update
    void Start()
    {
        // Start the game by printing instructions
        Debug.Log("Welcome to the Dentist Game!");
        Debug.Log("Your job is to treat your patient's tooth.");
        Debug.Log("Meet your patient: " + patientName);
        Debug.Log("Let's check if Timmy has a toothache!");

        // Checking the tooth status
        CheckTooth();
    }

    void CheckTooth()
    {
        // If statement (conditional check)
        if (toothStatus == 1)
        {
            Debug.Log("Oh no! Timmy has a rotten tooth!");
            Debug.Log("You need to treat Timmy's tooth.");
   


    }
}
