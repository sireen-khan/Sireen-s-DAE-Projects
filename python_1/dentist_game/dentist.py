def main():
    # Start the program
    print("Welcome to the Animal Dentist Game!")
    
    # Ask user if they are okay with using sharp tools for animals
    consent = input("Are you okay with using sharp tools for animals? (yes/no): ").strip().lower()
    
    if consent != "yes":
        print("Sorry, you can't play this game. Goodbye!")
        return
    
    # List of animals
    animals = {
        "dog": "I have a toothache!",
        "cat": "I need a dental cleaning!",
        "rabbit": "My tooth is too long!",
        "hamster": "I can't eat because my tooth hurts!"
    }
    
    while True:
        # Ask user to pick an animal
        print("\nAvailable animals to operate on:")
        for animal in animals.keys():
            print(f"- {animal}")
        
        animal_choice = input("Pick an animal you want to operate on: ").strip().lower()
        
        if animal_choice in animals:
            print(f"\n{animal_choice.capitalize()} says: {animals[animal_choice]}")
            print("You will use your tools to help take away the animal’s pain.")
            # Simulating the operation (for simplicity, just a message)
            input("Press Enter when you are done helping the animal...")
            print(f"You have successfully helped the {animal_choice}!")
        else:
            print("Invalid choice. Please choose a valid animal.")
            continue
        
        # Ask if user wants to continue
        continue_choice = input("Do you want to help another animal? (yes/no): ").strip().lower()
        if continue_choice != "yes":
            print("Thank you for playing! Goodbye!")
            break

# Run the game
if __name__ == "__main__":
    main()