namespace Final_Project;

class Program
{
    static void Main(string[] args)
    {
        bool gameOver = true;
        
        
        while (gameOver)
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Welcome to the Lost Doors");
            Console.WriteLine("How do you want to start?");
            Console.WriteLine("1) New Game");
            Console.WriteLine("2) Continue");
            Console.WriteLine("3) Exit");
            Console.Write(">>> ");
            int start = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("---------------------------------------------------");

            switch (start)
            {
                case 1:

                continue;
                
                case 2:
                
                continue;

                case 3:
                Console.WriteLine("You have exited!");
                break;
            }

        } 
    }
}
