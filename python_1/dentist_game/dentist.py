import random

def show_welcome_message():
    """Display the welcome message to the user."""  # Displays a welcome message to introduce the game
    print("Welcome to the Dentist Game!")

def check_user_consent():
    """Check if the user is okay with using sharp tools for animals.  # Continuously prompts until user answers 'yes' or 'no'
    
    Returns:
        bool: True if the user consents, False otherwise.
    """
    while True:
        consent = input("Are you okay with using sharp tools for animals? (yes/no): ").strip().lower()
        if consent in ['yes', 'no']:
            return consent == 'yes'
        print("Please answer with 'yes' or 'no'.")

def get_username():
    """Prompt the user to enter their username.  # Asks for and returns the user's name
    
    Returns:
        str: The username entered by the user.
    """
    return input("Please enter your username: ")

def display_animals(animals):
    """Display the list of animals available for treatment.  # Shows available animals to choose from
    
    Args:
        animals (dict): A dictionary of animals and their problems.
    """
    print("\nAvailable animals to operate on:")
    for animal in animals.keys():
        print(f"- {animal}")

def get_animal_choice(animals):
    """Prompt the user to choose an animal to operate on.  # Ensures valid animal selection through repeated prompts
    
    Args:
        animals (dict): A dictionary of animals.
        
    Returns:
        str: The chosen animal.
    """
    while True:
        chosen_animal = input("Pick an animal to operate on: ").strip().lower()
        if chosen_animal in animals:
            return chosen_animal
        print("Invalid animal choice. Please try again.")

def treat_animal(animal, problem):
    """Simulate treating the animal's dental problem.  # Displays the animal's problem and treatment result
    
    Args:
        animal (str): The animal being treated.
        problem (str): The specific problem the animal has.
    """
    print(f"{animal.capitalize()} says: {problem}")
    print("Using your dental tools to help the animal...")
    print(f"You have successfully treated the {animal}!")

def ask_to_continue():
    """Ask the user if they want to continue playing.  # Prompts until a valid response is given
    
    Returns:
        bool: True if the user wants to continue, False otherwise.
    """
    while True:
        continue_game = input("Do you want to help another animal? (yes/no): ").strip().lower()
        if continue_game in ['yes', 'no']:
            return continue_game == 'yes'
        print("Please answer with 'yes' or 'no'.")

def generate_animal_problems():
    """Generate a new dictionary of animals and their corresponding problems.  # Provides random animal names with specific dental issues
    
    Returns:
        dict: A dictionary with animal names as keys and their problems as values.
    """
    animals = [
        ('dog', "I have a toothache!"),
        ('cat', "My teeth are too sharp!"),
        ('rabbit', "I can't chew my food properly!"),
        ('hamster', "I have a broken tooth!"),
        ('parrot', "I have a beak problem!"),
        ('guinea pig', "I have a cavity!"),
        ('ferret', "My teeth hurt!"),
        ('turtle', "I have a shell issue!")
    ]
    random_animals = random.sample(animals, 4)  # Randomly select 4 animals from the list
    return dict(random_animals)

def main():
    """Main function to run the Dentist Game.  # Orchestrates the game flow from start to finish
    
    Steps:
        1. Show welcome message
        2. Check user consent for using sharp tools
        3. Get username
        4. Allow user to choose an animal and treat it
        5. Ask if the user wants to continue
    """
    show_welcome_message()  # Step 1: Display the welcome message

    # Step 2: Check if the user is okay with using sharp tools
    if not check_user_consent():
        print("Sorry, you can't play this game.")  # Ends the game if user does not consent
        return

    # Step 3: Get the user's username
    username = get_username()  # Prompts for the user's name
    print(f"Hello, {username}! Let's get started.")  # Greets the user

    total_treatments = 0  # Variable to count total treatments

    while True:  # Step 4: Loop to allow multiple treatments
        animals = generate_animal_problems()  # Generate new animal problems
        display_animals(animals)  # Show available animals
        chosen_animal = get_animal_choice(animals)  # Get the user's choice of animal

        # Step 5: Treat the chosen animal
        treat_animal(chosen_animal, animals[chosen_animal])  # Treat the selected animal
        total_treatments += 1  # Increment total treatments

        # Step 6: Ask if the user wants to continue
        if not ask_to_continue():
            print(f"Thank you for playing, {username}! You treated {total_treatments} animals.")  # Exits the game
            break  # Breaks the loop and ends the game

if __name__ == "__main__":
    main()  # Runs the main function to start the game
