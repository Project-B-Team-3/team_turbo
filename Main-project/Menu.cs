namespace Main_project
{
    public class Menu
    {
        public static void Main()
        {
            bool x = false;
            while (x == false)
            {
                // Geef alle hoofd-opties weer.
                Console.WriteLine("What do you wish to do?\n[1] View all upcoming flights\n[2] Book a flight\n[3] Cancel a flight\n[4] Quit");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("View all upcoming flights");
                        break;
                    case "2":
                        Console.WriteLine("Book a flight");
                        break;
                    case "3":
                        Console.WriteLine("Cancel a flight");
                        break;
                    case "4":
                        Console.WriteLine("Program has been quit");
                        x = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}
