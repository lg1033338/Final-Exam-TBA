using System.Data.Common;
using System.Formats.Asn1;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Final_Project;

class Program
{
    // Global properties
    private const string GAME_FILE = "game.txt";
    private static List<Player> Players { get; set; } = [];
    private static List<Item> Items { get; set; } = [];
    private static List<Room> Rooms { get; set; } = [];
    private static Game Game { get; set; } = new Game();
    static void Main(string[] args)
    {
        // Pre-load all the game resources
        BuildGameResources();

        bool gameOver = true;
        
        while (true)
        {
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("Welcome to the Lost Doors");
            Console.WriteLine("How do you want to start?");
            Console.WriteLine("1) New Game");
            Console.WriteLine("2) Continue");
            Console.WriteLine("3) Save Game");
            Console.WriteLine("4) Exit");
            Console.Write(">>> ");
            string error_check = Console.ReadLine();

            if (error_check != "1" && error_check != "2" &&
            error_check != "3" && error_check != "4")
            {
               Console.WriteLine("Invalid input. Please enter 1, 2, 3, or 4.");
               continue;
            }

            int start = int.Parse(error_check);
            
            
            switch (start)
            {
                // Start a new game
                case 1:
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("Welcome to a New Game!");
            Console.WriteLine("=================================================================================================================");

                while (true)
                {
                Console.WriteLine("what would you like your player name to be?");
                Console.Write(">>> ");
                string playerName = Console.ReadLine()+"";
                Console.WriteLine("=================================================================================================================");

                if (playerName.Length == 0)
                {
                Console.WriteLine("Invalid name. Please enter a valid name.");
                Console.WriteLine("=================================================================================================================");
                Console.WriteLine(" ");
                continue;
                }

                Player player = new Player { Name = playerName };

                // Persist the new player into the game state
                Game.Player = player;
                if (!Players.Contains(player))
                {
                    Players.Add(player);
                }

                // Set a sensible default starting room if any rooms exist
                if (Rooms.Count > 0)
                {
                    Game.CurrentRoom = Rooms[0];
                }
                break;
                    }

                Console.WriteLine("New game started. Player and starting room saved.");
                Console.WriteLine("=================================================================================================================");
                Console.WriteLine(" ");
                bool intro = true;

                 while (true)
                    {
                    Console.WriteLine("Would you like to Skip the into (y/n)?");
                    Console.Write(">>> ");
                     string skip = Console.ReadLine()+"";

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

                    while (intro)
                    {
            Console.WriteLine("=================================================================================================================");
                        Console.WriteLine("You were walking home late at night from work then suddenly you got kidnap by a mad scientist.");
                        Console.WriteLine("You woke up in a circle of 5 doors, He told you that to escape the place you need to find the keys to unlock the ladder to escape.");

                        break;
                    }
                    break;         
                // Load last played game
                case 2:
                LoadGameState();
                continue;

                // Save current game state (just an example, can be removed)
                case 3:
                Game.Player = Players[1];
                Game.CurrentRoom = Rooms[1];
                SaveGameState();
                continue;

                case 4:
            Console.WriteLine("=================================================================================================================");
                Console.WriteLine("You have exited!");
            Console.WriteLine("=================================================================================================================");
                break;

                default:
            Console.WriteLine("=================================================================================================================");
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
            Console.WriteLine("=================================================================================================================");
                continue;
            }
            while (true)
            {
            string[] key = {};
            Console.WriteLine("");
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("please choose a door to enter (1-10): ");
            Console.WriteLine("Inventory: ");///idk figure out how to show inventory or number of keys collected
            Console.Write(">>> ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("=================================================================================================================");
            
            if (error_check != "1" && error_check != "2" && error_check != "3" && error_check != "4" && error_check != "5" &&error_check != "6" && error_check != "7" && error_check != "8" && error_check != "9" && error_check != "10")
            {
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("Invalid input. Please enter 1-10");
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("");
             continue;

            }

            switch (choice)
                    {
                        case 1:
                        Random rand = new Random();
                        int number = rand.Next(0,2000000000);
                        int guess = 0;
                        Console.WriteLine(" ");
                        Console.WriteLine("=================================================================================================================");
                        Console.WriteLine("This dungeon test your luck and patient");
                        Console.WriteLine("get the correct number to get a piece of the key");
                        Console.WriteLine("=================================================================================================================");
                        Console.WriteLine(" ");

                        while (true)
                        {
                        Console.WriteLine("enter 1 to start enter 2 to exit");
                        Console.Write(">>> ");
                        string answer = Console.ReadLine()+"";
                        
                        if (answer == "1")
                            {
                        while (guess != number)
                        {
                            guess++;
                            Console.WriteLine(guess);
                        }

                            Console.WriteLine("Found the number! It was: " + number);
                            ///somthing like congrat you found a piece of the key add it to the inventory
                              break;
                            }
                        else if (answer == "2")
                            {
                                break;
                            }
                        else
                            {
                        Console.WriteLine("=================================================================================================================");
                        Console.WriteLine("Invalid input. Please enter 1 or 2.");
                        Console.WriteLine("=================================================================================================================");


                                continue;
                            }
                        }
                        break;

    case 2:
    {
        Random door = new Random();
        int streak = 0;

        Console.WriteLine("=================================================================================================================");
        Console.WriteLine("Guess the correct door (1, 2, or 3)!");
        Console.WriteLine("Get 10 correct guesses to collect a piece of the key");
        Console.WriteLine("=================================================================================================================");

        while (streak < 10)
        {
            int correctDoor = door.Next(1, 4); // 1–3

            Console.Write("Pick a door (1-3): ");
            int Guess = int.Parse(Console.ReadLine()+"");

            if (Guess == correctDoor)
            {
                streak++;
                Console.WriteLine("Correct! Streak: " + streak);
            }
            else
            {
                streak = 0;
                Console.WriteLine("Wrong! It was door " + correctDoor);
            }
        }
        /// you found a piece of the key add it to the inventory
        Console.WriteLine("You win!");
        break;
    }

                        case 3:
                        Console.WriteLine("=================================================================================================================");

                        Console.WriteLine(" ");

                        Console.WriteLine("This room you have to win rock paper scissors against the computer to get a piece of the key");
                        Console.WriteLine(" ");
                        Console.WriteLine("=================================================================================================================");


                        Console.WriteLine("But their a twist!!");
                        Console.WriteLine(" ");
                        Console.WriteLine("We also added lizard and spock");
                        Console.WriteLine(" ");
                        Console.WriteLine("Rock beat: scissors and lizard");
                        Console.WriteLine(" ");

                        Console.WriteLine("Paper beat: rock and spock");
                        Console.WriteLine(" ");

                        Console.WriteLine("scissors beat: paper and lizard");
                        Console.WriteLine(" ");

                        Console.WriteLine("spock beat: scissors and rock");
                        Console.WriteLine(" ");

                        Console.WriteLine("lizard beat: spock and paper");
                        Console.WriteLine(" ");
                        Console.WriteLine("=================================================================================================================");



                         while (true)
                        {
                        Console.WriteLine("enter 1 to start enter 2 to exit");
                        Console.Write(">>> ");
                        string answer = Console.ReadLine()+"";
                        if (answer == "1")
                            {
                                string[] Rock_paper_Scissors_add_onn = { "rock", "paper", "scissors", "lizard", "spock" };
                                Random game = new Random();
                                int playerScore = 0;
                                int computerScore = 0;

                                while (playerScore < 3 && computerScore < 3)
                                {
                                    Console.Write("Enter rock, paper, scissors, lizard, or spock: ");
                                    string playerChoice = Console.ReadLine()+"".ToLower();
                                    int computer = game.Next(Rock_paper_Scissors_add_onn.Length);
                                    string computerChoice = Rock_paper_Scissors_add_onn[computer];
                                    Console.WriteLine("Computer chose: " + computerChoice);

                                    if (playerChoice == computerChoice)
                                    {
                                        Console.WriteLine("It's a tie!");
                                    }
                                    else if ((playerChoice == "rock" && (computerChoice == "scissors" || computerChoice == "lizard")) ||
                                             (playerChoice == "paper" && (computerChoice == "rock" || computerChoice == "spock")) ||
                                             (playerChoice == "scissors" && (computerChoice == "paper" || computerChoice == "lizard")) ||
                                             (playerChoice == "lizard" && (computerChoice == "spock" || computerChoice == "paper")) ||
                                             (playerChoice == "spock" && (computerChoice == "scissors" || computerChoice == "rock")))
                                    {
                                        playerScore++;
                                        Console.WriteLine("You win this round! Score: " + playerScore + "-" + computerScore);
                                    }
                                    else
                                    {
                                        computerScore++;
                                        Console.WriteLine("Computer wins this round! Score: " + playerScore + "-" + computerScore);
                                    }
                                }

                                if (playerScore == 3)
                                {
                                    Console.WriteLine("You won the game!");
                                    ///you found a piece of the key add it to the inventory
                                }
                                else
                                {
                                    Console.WriteLine("Computer won the game!");
                                }
                                break;
                            }
                        else if (answer == "2")
                            {
                                break;
                            }
                        else
                            {
                                Console.WriteLine("Invalid input. Please enter 1 or 2.");
                                continue;
                            }
                        }
                        break;

                        case 4:
                        break;
                        
                        case 5:
                        break;

                        default:
                        continue;
                    }
            }

        } 
    }

    #region IO

    // Load the game state from a file
    static void LoadGameState()
    {
        string contents = File.ReadAllText(GAME_FILE);
        
        Console.WriteLine("Previously Saved Game: " + contents);

        string[] components = contents.Split(';');

        if (components.Length > 0)
        {
            string[] playerData = components[0].Split('=');
            if (playerData.Length > 1)
            {
                foreach (var player in Players)
                {
                    if (player.Name == playerData[1])
                    {
                        Game.Player = player;
                        break;
                    }
                }
            }

            string[] roomData = components[1].Split('=');
            if (roomData.Length > 1)
            {
                foreach (var room in Rooms)
                {
                    if (room.Name == roomData[1])
                    {
                        Game.CurrentRoom = room;
                        break;
                    }
                }
            }

            string[] itemsData = components.Length > 2 ? components[2].Split('=') : Array.Empty<string>();
            if (itemsData.Length > 1)
            {
                string[] itemNames = itemsData[1].Split(',');
                foreach (var itemName in itemNames)
                {
                    foreach (var item in Items)
                    {
                        if (item.Name == itemName)
                        {
                            Game.CollectedItems.Add(item);
                            break;
                        }
                    }
                }
            }
            
        }

    }

    // Save the game state to a file
    static void SaveGameState()
    {
        string content = $"Player={Game.Player.Name};CurrentRoom={Game.CurrentRoom.Name}";

        if (Game.CollectedItems.Count > 0)
        {
            content += ";CollectedItems=";

            foreach (var item in Game.CollectedItems)
            {
                content += item.Name + ",";
            }
        }

        File.WriteAllText(GAME_FILE, content);
    }

    #endregion 

    #region Game Resources

    static void BuildGameResources()
    {
        CreatePlayers();
        CreateItems();
        CreateRooms();   
    }

    static void CreatePlayers()
    {
        Players =
        [
            new() { Name = "Bob"},
            new() { Name = "Jim"},
            new() { Name = "Sally"}
        ];
    }

    static void CreateItems()
    {
        Items =
        [
            new() { Name = "Spoon"},
            new() { Name = "Hat"},
            new() { Name = "Lamp"}
        ];
    }


    static void CreateRooms()
    {
        Rooms =
        [
            new() { Name = "Bedroom"},
            new() { Name = "Bathroom"},
            new() { Name = "Kitchen"},
        ];
    }

    #endregion
}

public class Player
{
    public required string Name { get; set; }
}

public class Room
{
    public required string Name { get; set; }
}

public class Item
{
    public required string Name { get; set;}
}

public class Game
{    
    public Player Player { get; set; }
    public Room CurrentRoom { get; set; }
    public List<Item> CollectedItems { get; set; } = [];
}