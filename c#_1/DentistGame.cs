using System.Collections;
using UnityEngine;

public class DentistGame : MonoBehaviour
{
    // Array of patient names
    public string[] patientNames = { "Timmy", "Alice", "Bob", "Sara" };

    // List of tooth statuses for each patient (1 = rotten, 0 = healthy)
    private int[] patientTeethStatuses;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the array to match the number of patients
        patientTeethStatuses = new int[patientNames.Length];

        // Randomly assign some rotten teeth (1) and healthy teeth (0)
        AssignToothStatuses();

        // Start the game by printing instructions
        Debug.Log("Welcome to the Dentist Game!");
        
        // Start treating each patient
        TreatPatients();
    }

    // Randomly assigns each patient a mix of healthy and rotten teeth
    void AssignToothStatuses()
    {
        for (int i = 0; i < patientTeethStatuses.Length; i++)
        {
            patientTeethStatuses[i] = Random.Range(0, 2); // Randomly set to 0 (healthy) or 1 (rotten)
        }
    }

    // Treats each patient
    void TreatPatients()
    {
        // Loop through each patient
        for (int i = 0; i < patientNames.Length; i++)
        {
            Debug.Log("It's time to treat " + patientNames[i]);

            // Check the patient's tooth
            CheckTooth(i);

            // After checking the tooth, treat it if necessary
            if (patientTeethStatuses[i] == 1)
            {
                TreatTooth(i);
            }
            else
            {
                Debug.Log(patientNames[i] + " has no tooth problems. Great job!");
            }
        }
    }

    // Checks the status of a patient's tooth
    void CheckTooth(int patientIndex)
    {
        if (patientTeethStatuses[patientIndex] == 1)
        {
            Debug.Log(patientNames[patientIndex] + " has a rotten tooth!");
        }
        else
        {
            Debug.Log(patientNames[patientIndex] + " has a healthy tooth.");
        }
    }

    // Simulate treating a rotten tooth (random chance of success)
    void TreatTooth(int patientIndex)
    {
        Debug.Log("Treating " + patientNames[patientIndex] + "'s rotten tooth...");

        // Simulate a treatment outcome with a random success chance
        bool treatmentSuccessful = Random.Range(0, 2) == 1;

        if (treatmentSuccessful)
        {
            Debug.Log("Treatment was successful! " + patientNames[patientIndex] + "'s tooth is now healthy.");
            patientTeethStatuses[patientIndex] = 0; // Update the tooth status to healthy
        }
        else
        {
            Debug.Log("The treatment failed! " + patientNames[patientIndex] + "'s tooth is still rotten.");
        }
    }
}
using UnityEngine;

public class AnimalBehavior : MonoBehaviour
{
    public bool isBeingTreated = false;

    public void TreatAnimal()
    {
        isBeingTreated = true;
        // Add logic for animal reactions like animations or sound effects
    }

    public void StopTreatment()
    {
        isBeingTreated = false;
        // Reset animal reactions
    }
}

