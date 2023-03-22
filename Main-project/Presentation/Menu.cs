namespace Main_project.Presentation;
public class Menu
{
    public static void Start()
    {
        bool x = false;
        ConsoleKeyInfo key = new ConsoleKeyInfo();
        while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape &&  x == false)
        {
            Console.Clear();
            Console.WriteLine("What do you wish to do?\n[1] View all upcoming flights\n[2] Book a flight\n[3] Cancel a flight\n[4] Quit\n");
            key = Console.ReadKey(true);
                //Press key to trigger event ( D0 = 0 , D1 = 1 etc.)
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Viewing all upcoming flights");
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("Book a flight");
                        break;

                    case ConsoleKey.D3:
                        Console.WriteLine("Cancel a flight");
                        break;

                    case ConsoleKey.D4:
                        Console.WriteLine("Program has been quit");
                        x = true;
                        break;

                    default:
                        // Checks for capslock/numlock
                        if (Console.CapsLock && Console.NumberLock)
                        {
                            Console.WriteLine(key.KeyChar);
                            Console.Write("Invalid option");
                        }
                        break;
                        {

                        }
                }
        }

    }
}
