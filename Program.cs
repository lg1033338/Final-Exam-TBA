using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace Final_Project;

// Simple class to hold the player's name
class Person
{ 
    public string Name;
}

// Class that manages the player's key fragments (inventory)
public class KeyInventory
{
    private List<string> keyPieces = new List<string>(); // list of key items ("Piece1", "Piece2" etc.)

    // Add a key fragment to the inventory and show how many you have
    public void AddKey(string keyName)
    {
        keyPieces.Add(keyName);
        Console.WriteLine($"You obtained a key fragment: {keyName}  ({keyPieces.Count}/4)");
    }

    // Return how many key fragments we currently have
    public int Count()
    {
        return keyPieces.Count;
    }

    // Display list of all key fragments collected
    public void Display()
    {
        Console.WriteLine("Your Key Inventory:");
        if (keyPieces.Count == 0)
        {
            Console.WriteLine("  (empty — you have no key fragments yet)");
        }
        else
        {
            foreach (string key in keyPieces)
                Console.WriteLine($" - {key}");
        }
    }

    // Check if we have at least totalRequired key fragments
    public bool HasAllKeys(int totalRequired)
    {
        return keyPieces.Count >= totalRequired;
    }
}

class Program
{
    // GLOBAL game state
    static KeyInventory Keys = new KeyInventory();  // holds all key fragments
    static Person CurrentPlayer = new Person();     // holds player name
    
    /*
     * HIGH-LEVEL PSEUDOCODE
     * 
     * Main():
     *   loop forever:
     *     show main menu (New Game / Exit)
     *     get player choice
     *     if Start:
     *         ask for player name
     *         (optional) show intro story
     *         go into main door loop
     *     if Exit:
     *         quit program
     * 
     * Main Door Loop:
     *   loop:
     *     show number of key fragments
     *     show list of 5 doors (each is a mini-game / trial)
     *     read choice
     *     switch on door choice:
     *       case 1: luck-based counting machine trial → key fragment 1
     *       case 2: guess-the-door streak trial → key fragment 2
     *       case 3: Rock-Paper-Scissors-Lizard-Spock trial → key fragment 3
     *       case 4: Hangman / word puzzle using file word list → key fragment 4
     *       case 5: final story door – if 4 keys → moral choice → ending & exit
     */

    static void Main(string[] args)
    {
        // ===== MAIN MENU LOOP =====
        while (true)
        {
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("Welcome to the Lost Doors");
            Console.WriteLine("How do you want to start?");
            Console.WriteLine("1) Start");
            Console.WriteLine("2) Exit");
            Console.Write(">>> ");
            string? error_check = Console.ReadLine();

            // Validate main menu input (must be 1 or 2)
            if (error_check != "1" && error_check != "2")
            {
               Console.WriteLine("Invalid input. Please enter 1 or 2.");
               continue; // restart menu loop
            }

            int start = int.Parse(error_check);
                        
            switch (start)
            {
                // ==========================
                // CASE 1: Start a new game
                // ==========================
                case 1:
                    Console.WriteLine("=================================================================================================================");
                    Console.WriteLine("Welcome to a New Game!");
                    Console.WriteLine("=================================================================================================================");

                    // Ask for player name until valid
                    while (true)
                    {
                        Console.WriteLine("What would you like your player name to be?");
                        Console.Write(">>> ");
                        CurrentPlayer.Name = Console.ReadLine() ?? "";
                        Console.WriteLine("=================================================================================================================");

                        if (CurrentPlayer.Name.Length == 0)
                        {
                            Console.WriteLine("Invalid name. Please enter a valid name.");
                            Console.WriteLine("=================================================================================================================");
                            Console.WriteLine(" ");
                            continue; // ask again
                        }

                        break; // name is valid
                    }

                    Console.WriteLine($"New game started. You slowly remember… your name is {CurrentPlayer.Name}.");
                    Console.WriteLine("=================================================================================================================");
                    Console.WriteLine(" ");
                    bool intro = true;

                    // Ask if player wants to skip intro story
                    while (true)
                    {
                        Console.WriteLine("Would you like to skip the intro (y/n)?");
                        Console.Write(">>> ");
                        string skip = (Console.ReadLine() ?? "") + "";

                        if (skip.ToLower() == "y")
                        {
                            intro = false;
                            break;
                        }
                        if (skip.ToLower() == "n")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("=================================================================================================================");
                            Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                            Console.WriteLine("=================================================================================================================");
                            continue;
                        }
                    }

                    // Show intro story if not skipped
                    while (intro)
                    {
                        Console.WriteLine("=================================================================================================================");
                        Console.WriteLine("You were walking home late at night, the streets empty and quiet.");
                        Console.WriteLine("A van pulled up. A needle. Darkness.");
                        Console.WriteLine();
                        Console.WriteLine("You wake up on a cold metal floor.");
                        Console.WriteLine("Around you: a perfect circle of heavy doors, each marked with a glowing symbol.");
                        Console.WriteLine("A distorted voice crackles from a speaker above you:");
                        Console.WriteLine("\"Welcome to my experiment, test subject. If you want to escape, you must earn four key fragments.\"");
                        Console.WriteLine("\"Each door holds a trial. Survive them, collect the fragments, and maybe I'll let you climb the ladder to freedom.\"");
                        Console.WriteLine("=================================================================================================================");
                        Console.WriteLine("Press ENTER to stand up and look at the doors...");
                        Console.ReadLine();
                        break;
                    }
                    break;         

                // ==========================
                // CASE 2: Exit game
                // ==========================
                case 2:
                    Console.WriteLine("=================================================================================================================");
                    Console.WriteLine("You chose not to enter the circle of doors... for now.");
                    Console.WriteLine("=================================================================================================================");
                    return; // end program

                default:
                    Console.WriteLine("=================================================================================================================");
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 2.");
                    Console.WriteLine("=================================================================================================================");
                    continue;
            }

            // ==========================
            // MAIN DOOR SELECTION LOOP
            // ==========================
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("=================================================================================================================");
                Console.WriteLine("You stand once more in the circular chamber of doors.");
                Console.WriteLine($"Key Fragments Collected: {Keys.Count()}/4");
                Console.WriteLine("The doors around you flicker with dim light.");
                Console.WriteLine("Choose a door to enter:");
                Console.WriteLine("1) The Endless Counter (Luck Trial)");
                Console.WriteLine("2) The Shifting Hall (Guess-the-Door)");
                Console.WriteLine("3) The Hand of Choices (RPS-Lizard-Spock)");
                Console.WriteLine("4) The Word Lab (Encrypted Terminal)");
                Console.WriteLine("5) The Final Door (Exit or Sacrifice)");
                Console.Write(">>> ");
                string? choiceInput = Console.ReadLine();
                Console.WriteLine("=================================================================================================================");

                // Validate door selection (1–5)
                if (!int.TryParse(choiceInput, out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                    continue;
                }
                
                if (choice < 1 || choice > 5)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                    continue;
                }

                // ==========================
                // HANDLE EACH DOOR
                // ==========================
                switch (choice)
                {
                    // -------------------------------------------------
                    // DOOR 1: The Endless Counter (pure patience/luck)
                    // -------------------------------------------------
                    case 1:
                        {
                            Random rand = new Random();
                            int number = rand.Next(0, 2000000000); // secret target number
                            int guess = 0;
                            Console.WriteLine(" ");
                            Console.WriteLine("=================================================================================================================");
                            Console.WriteLine("You enter a room dominated by a colossal mechanical counter stretching to the ceiling.");
                            Console.WriteLine("The scientist’s voice echoes: \"Patience is a virtue, and also a curse.\"");
                            Console.WriteLine("\"Somewhere between 0 and 2 billion is the number I've marked. Let the machine search.\"");
                            Console.WriteLine("If you endure the process, a key fragment will be yours.");
                            Console.WriteLine("=================================================================================================================");
                            Console.WriteLine(" ");

                            // Inner loop for this door trial
                            while (true)
                            {
                                Console.WriteLine("Enter 1 to start the counting machine, or 2 to step back out.");
                                Console.Write(">>> ");
                                string answer = Console.ReadLine() + "";
                                
                                if (answer == "1")
                                {
                                    Console.WriteLine("The machine whirs to life, cycling through numbers at inhuman speed...");
                                    // Simple loop counting up until number is matched
                                    while (guess != number)
                                    {
                                        guess++;
                                        Console.WriteLine(guess);
                                    }

                                    Console.WriteLine("The machine chimes loudly.");
                                    Console.WriteLine("It stops on the marked number: " + number);
                                    Console.WriteLine("A compartment opens, revealing a small glowing shard of metal.");
                                    Keys.AddKey("Fragment Door 1");
                                    break; // leave this door
                                }
                                else if (answer == "2")
                                {
                                    Console.WriteLine("You step away from the roaring machine, your ears ringing.");
                                    break; // leave this door without reward
                                }
                                else
                                {
                                    Console.WriteLine("=================================================================================================================");
                                    Console.WriteLine("The speakers crackle: \"I said 1 or 2. Focus.\"");
                                    Console.WriteLine("=================================================================================================================");
                                    continue;
                                }
                            }
                            break;
                        }

                    // -------------------------------------------------
                    // DOOR 2: The Shifting Hall (guess-the-door streak)
                    // -------------------------------------------------
                    case 2:
                    {
                        Random door = new Random();
                        int streak = 0;

                        Console.WriteLine("=================================================================================================================");
                        Console.WriteLine("You step into a long hallway with three doors at the far end.");
                        Console.WriteLine("Each time you choose, the hallway shifts, and the doors rearrange.");
                        Console.WriteLine("\"Predict the correct door,\" the scientist says. \"Do it ten times in a row.\"");
                        Console.WriteLine("\"Only then will I reward you.\"");
                        Console.WriteLine("=================================================================================================================");

                        // Keep guessing doors until player gets 10 hits in a row
                        while (streak < 10)
                        {
                            int correctDoor = door.Next(1, 4); // 1–3

                            Console.Write("Pick a door (1-3): ");
                            string? guessInput = Console.ReadLine();
                            if (!int.TryParse(guessInput, out int Guess))
                            {
                                Console.WriteLine("That's not even a number. The doors silently reset.");
                                continue;
                            }

                            if (Guess < 1 || Guess > 3)
                            {
                                Console.WriteLine("The hallway rejects your nonsense choice. Doors 1, 2, or 3 only.");
                                continue;
                            }

                            if (Guess == correctDoor)
                            {
                                streak++;
                                Console.WriteLine($"The door swings open to an empty space, then seals again. Streak: {streak}/10");
                            }
                            else
                            {
                                streak = 0;
                                Console.WriteLine($"You open the door to find a solid wall. A buzzer sounds. The correct door was {correctDoor}.");
                                Console.WriteLine("Your streak has been reset.");
                            }
                        }

                        // Player completed the streak → reward key fragment
                        Console.WriteLine("Finally, one door opens to reveal a pedestal.");
                        Console.WriteLine("On it lies a jagged KEY FRAGMENT. You take it.");
                        Keys.AddKey("Fragment Door 2");
                        break;
                    }

                    // -------------------------------------------------
                    // DOOR 3: RPS-Lizard-Spock arena (first to 3)
                    // -------------------------------------------------
                    case 3:
                        Console.WriteLine("=================================================================================================================");
                        Console.WriteLine("You enter a circular arena. Holographic hands hover in the air around you.");
                        Console.WriteLine("Rock. Paper. Scissors. Lizard. Spock.");
                        Console.WriteLine("\"Let’s see how you handle complex choices under pressure,\" the scientist muses.");
                        Console.WriteLine("\"First to three wins takes the fragment.\"");
                        Console.WriteLine("=================================================================================================================");
                        Console.WriteLine("Rock beats: Scissors and Lizard");
                        Console.WriteLine("Paper beats: Rock and Spock");
                        Console.WriteLine("Scissors beats: Paper and Lizard");
                        Console.WriteLine("Spock beats: Scissors and Rock");
                        Console.WriteLine("Lizard beats: Spock and Paper");
                        Console.WriteLine("=================================================================================================================");

                        // Outer loop to either start or leave
                        while (true)
                        {
                            Console.WriteLine("Enter 1 to begin the match, or 2 to leave the room.");
                            Console.Write(">>> ");
                            string answer = Console.ReadLine() + "";
                            if (answer == "1")
                            {
                                string[] Rock_paper_Scissors_add_onn = { "rock", "paper", "scissors", "lizard", "spock" };
                                Random game = new Random();
                                int playerScore = 0;
                                int computerScore = 0;

                                // RPSLS rounds until either side hits 3 points
                                while (playerScore < 3 && computerScore < 3)
                                {
                                    Console.Write("Choose your sign (rock, paper, scissors, lizard, spock): ");
                                    string playerChoice = (Console.ReadLine() ?? "").ToLower();
                                    int computer = game.Next(Rock_paper_Scissors_add_onn.Length);
                                    string computerChoice = Rock_paper_Scissors_add_onn[computer];
                                    Console.WriteLine("The hologram forms: " + computerChoice);

                                    // Validate player's choice
                                    if (!Rock_paper_Scissors_add_onn.Contains(playerChoice))
                                    {
                                        Console.WriteLine("The system fails to recognize your input. Focus and choose one of the valid signs.");
                                        continue;
                                    }

                                    // Determine outcome
                                    if (playerChoice == computerChoice)
                                    {
                                        Console.WriteLine("The images clash and dissolve. A tie.");
                                    }
                                    else if ((playerChoice == "rock" && (computerChoice == "scissors" || computerChoice == "lizard")) ||
                                             (playerChoice == "paper" && (computerChoice == "rock" || computerChoice == "spock")) ||
                                             (playerChoice == "scissors" && (computerChoice == "paper" || computerChoice == "lizard")) ||
                                             (playerChoice == "lizard" && (computerChoice == "spock" || computerChoice == "paper")) ||
                                             (playerChoice == "spock" && (computerChoice == "scissors" || computerChoice == "rock")))
                                    {
                                        playerScore++;
                                        Console.WriteLine("Your sign overwhelms the hologram.");
                                        Console.WriteLine("Score: " + playerScore + " - " + computerScore);
                                    }
                                    else
                                    {
                                        computerScore++;
                                        Console.WriteLine("Your sign shatters. The machine scores a point.");
                                        Console.WriteLine("Score: " + playerScore + " - " + computerScore);
                                    }
                                }

                                // Check who won the match
                                if (playerScore == 3)
                                {
                                    Console.WriteLine("A panel opens in the arena floor, revealing a glowing KEY FRAGMENT.");
                                    Keys.AddKey("Fragment Door 3");
                                }
                                else
                                {
                                    Console.WriteLine("The arena powers down. \"Come back when you're ready to think,\" the voice taunts.");
                                }
                                break;
                            }
                            else if (answer == "2")
                            {
                                Console.WriteLine("You leave the arena, the holograms flickering out behind you.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter 1 or 2.");
                                continue;
                            }
                        }
                        break;

                    // -------------------------------------------------
                    // DOOR 4: Word Lab (Hangman-style encrypted word)
                    // -------------------------------------------------
                    case 4:
                        while (true)
                        {
                            // Read word list for the test from external file
                            List<string> WordBank = File.ReadAllLines("Word List.txt").ToList();
                            Random rng = new Random();
                            string chosenWord = WordBank[rng.Next(WordBank.Count)]; // random word

                            Console.WriteLine("");
                            Console.WriteLine("=================================================================================================================");
                            Console.WriteLine("Door 4 slides open with a hiss. Inside is a dark room lit only by a hanging screen.");
                            Console.WriteLine("Lines of scrambled text flicker across it, distorted and broken.");
                            Console.WriteLine("A distorted voice echoes through hidden speakers...");
                            Console.WriteLine("\"Your mind is your last weapon. Decode the word correctly... or be punished.\"");
                            Console.WriteLine("=================================================================================================================");

                            int wordSize = chosenWord.Length;
                            int livesRemaining = 7;

                            // Build masked word (replace letters with underscores, keep spaces)
                            string display = "";
                            for (int i = 0; i < wordSize; i++)
                            {
                                if (chosenWord[i] == ' ')
                                    display += " ";
                                else
                                    display += "_";
                            }

                            // Hangman-style game loop
                            while (livesRemaining > 0)
                            {
                                Console.WriteLine("");
                                Console.WriteLine($"Remaining Attempts: {livesRemaining}");
                                Console.WriteLine("Encrypted Word: " + display);
                                Console.Write("Enter your guess (single letter or full word): ");

                                string? input = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(input))
                                {
                                    Console.WriteLine("The room grows colder... a warning tone sounds. Speak, or perish.");
                                    continue;
                                }

                                // FULL WORD GUESS
                                if (input.Length > 1)
                                {
                                    if (input.ToLower() == chosenWord.ToLower())
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("The screen flashes green. The locks disengage with a solid clunk.");
                                        Console.WriteLine($"Correct — The word was: {chosenWord}");
                                        Console.WriteLine("A hidden drawer opens beneath the terminal, revealing a KEY FRAGMENT.");
                                        Keys.AddKey("Fragment Door 4");
                                        break;
                                    }
                                    else
                                    {
                                        livesRemaining--;
                                        Console.WriteLine("Incorrect. The walls seem to inch closer around you...");
                                    }
                                }
                                else // SINGLE LETTER GUESS
                                {
                                    char singleGuess = char.ToLower(input[0]);
                                    bool found = false;
                                    string newDisplay = "";

                                    // Reveal letters if they match the guess
                                    for (int i = 0; i < wordSize; i++)
                                    {
                                        if (char.ToLower(chosenWord[i]) == singleGuess)
                                        {
                                            newDisplay += chosenWord[i];
                                            found = true;
                                        }
                                        else if (display[i] != '_')
                                        {
                                            newDisplay += display[i];
                                        }
                                        else
                                        {
                                            newDisplay += "_";
                                        }
                                    }

                                    display = newDisplay;

                                    if (!found)
                                    {
                                        livesRemaining--;
                                        Console.WriteLine("Wrong letter. A sharp alarm pierces your ears.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("New letters flicker into place on the screen...");
                                    }
                                }

                                // Check if all letters revealed = win
                                bool allRevealed = !display.Contains('_');
                                if (allRevealed)
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("The encrypted word fully resolves on the screen.");
                                    Console.WriteLine($"The answer was: {chosenWord}");
                                    Console.WriteLine("You grasp a KEY FRAGMENT from the unlocking panel.");
                                    Keys.AddKey("Fragment Door 4");
                                    break;
                                }
                            }

                            // Lose condition for Door 4
                            if (livesRemaining == 0)
                            {
                                Console.WriteLine("");
                                Console.WriteLine("The screen turns blood-red.");
                                Console.WriteLine($"\"Failure,\" the voice states coldly. The correct word was: {chosenWord}");
                                Console.WriteLine("The door seals shut — you are forced to retreat to the main chamber.");
                                continue; // can re-enter door 4 later with a new word
                            }
                            break; // exit door 4 once puzzle is completed
                        }
                        break;
                        
                    // -------------------------------------------------
                    // DOOR 5: Final Door (Moral Choice + Ending)
                    // -------------------------------------------------
                    case 5:
                        Console.WriteLine("You approach a massive metal door with a key-shaped indentation.");
    
                        // Check if player has all 4 fragments
                        if (!Keys.HasAllKeys(4))
                        {
                            Console.WriteLine($"You only have {Keys.Count()}/4 key fragments!");
                            Console.WriteLine("The mechanism rejects you. Cold metal presses back against your hand.");
                            Console.WriteLine("The scientist chuckles through the speakers: \"Come back when you've earned your freedom.\"");
                            break;
                        }

                        Console.WriteLine("The keys in your inventory begin to glow, resonating with the door.");
                        Console.WriteLine("They float from your hands, fusing together and reshaping into a single ornate key.");
                        Console.WriteLine("It drifts forward, inserts itself into the lock, and turns with a heavy click.");
                        Console.WriteLine("The Final Door slowly opens...");
                        Console.WriteLine("=================================================================================================================");
                        Console.WriteLine("You step into a vast, dimly lit lab.");
                        Console.WriteLine("Glass tubes line the walls. Inside them: other people, still unconscious.");
                        Console.WriteLine("A metal staircase leads up to a hatch marked: EXIT.");
                        Console.WriteLine("The mad scientist appears on a balcony above you, slow-clapping.");
                        Console.WriteLine("=================================================================================================================");
                        Console.WriteLine("\"Well done,\" he says. \"You solved my games, found my keys, survived my toys.\"");
                        Console.WriteLine("\"So here's my offer, test subject...\"");
                        Console.WriteLine();
                        Console.WriteLine("1) I open the exit for YOU only. You climb the ladder, go home, and forget this place.");
                        Console.WriteLine("   The others stay here. The experiments continue.");
                        Console.WriteLine();
                        Console.WriteLine("2) You refuse. You force me to release everyone, including you.");
                        Console.WriteLine("   But if you do... there is no guarantee you survive what happens next.");
                        Console.WriteLine("=================================================================================================================");

                        int finalChoice = 0;
                        // Read final choice until valid (1 or 2)
                        while (true)
                        {
                            Console.Write("Do you take the deal? (1 = Take it, 2 = Refuse): ");
                            string input = Console.ReadLine() ?? "";

                            if (input == "1" || input == "2")
                            {
                                finalChoice = int.Parse(input);
                                break;
                            }

                            Console.WriteLine("Invalid input. Please enter 1 or 2.");
                        }

                        // BAD END path (selfish)
                        if (finalChoice == 1)
                        {
                            Console.Write("Are you SURE you will leave everyone behind? (y/n): ");
                            string confirm = (Console.ReadLine() ?? "").ToLower();

                            if (confirm == "y")
                            {
                                Console.WriteLine("=================================================================================================================");
                                Console.WriteLine("You swallow hard and nod.");
                                Console.WriteLine("The scientist smiles. \"I knew you'd make the rational choice.\"");
                                Console.WriteLine("A spotlight snaps on above the ladder. The tubes hiss as the other test subjects");
                                Console.WriteLine("sink deeper into their restraints.");
                                Console.WriteLine();
                                Console.WriteLine("You climb. One rung. Then another. The lab grows smaller beneath you.");
                                Console.WriteLine("You don't look back.");
                                Console.WriteLine();
                                Console.WriteLine($"Even if you escaped, {CurrentPlayer.Name}, can you really live with that choice?");
                                Console.WriteLine("BAD ENDING: Survival At Any Cost.");
                                Console.WriteLine("=================================================================================================================");
                                Console.WriteLine("Press ENTER to exit the game...");
                                Console.ReadLine();
                                return; // end game
                            }
                            else
                            {
                                // Player backed out of bad choice → force into good path
                                finalChoice = 2;
                            }
                        }

                        // GOOD END path (save everyone)
                        if (finalChoice == 2)
                        {
                            Console.WriteLine("=================================================================================================================");
                            Console.WriteLine("You stare up at him and shake your head.");
                            Console.WriteLine("\"No deal,\" you say. \"Let everyone out. Now.\"");
                            Console.WriteLine();
                            Console.WriteLine("For the first time, he looks annoyed.");
                            Console.WriteLine("\"You weren't supposed to say that,\" he mutters. He presses something on a remote.");
                            Console.WriteLine("The tubes crack open. Alarms scream. Red lights strobe across the lab.");
                            Console.WriteLine();
                            Console.WriteLine("You slam the fused key into the hatch lock and twist.");
                            Console.WriteLine("Together, you and the other test subjects surge up and out, into the cold night air.");
                            Console.WriteLine();
                            Console.WriteLine($"You did the right thing, {CurrentPlayer.Name}. The others will never forget you.");
                            Console.WriteLine("GOOD ENDING: Nobody Gets Left Behind.");
                            Console.WriteLine("=================================================================================================================");
                            Console.WriteLine("Press ENTER to exit the game...");
                            Console.ReadLine();
                            return; // end game
                        }
                        
                        break;

                    // Should never hit this case due to earlier validation,
                    // but left as a fallback.
                    default:
                        Console.WriteLine("You look around, but nothing new reveals itself...");
                        continue;
                }
            }
        } 
    }
}