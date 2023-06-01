using Main_project.Misc;

namespace Main_project.Presentation;

public static class Menu
{
    public static void Start()
    {
        bool x = false;
        ConsoleKeyInfo key = new ConsoleKeyInfo();
        while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape && x == false)
        {
            Console.Clear();
            Console.WriteLine("What do you wish to do?\n[1] View all upcoming flights\n[2] Book a flight\n[3] Cancel a flight\n[4] Quit\n");
            
            key = Console.ReadKey(true);
            //Press key to trigger event ( D0 = 0 , D1 = 1 etc.)
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    ConsoleView.DisplayFlights();
                    break;
                case ConsoleKey.D2:
                    CreateBooking.CreateNewBooking();
                    break;

                case ConsoleKey.D3:
                    Console.WriteLine("Cancel a flight");
                    var reservationNumber = Console.ReadLine();
                    ChangeBooking.CancelBooking(reservationNumber);
                    break;

                case ConsoleKey.D4: case ConsoleKey.Q:
                    Console.WriteLine("Program has been quit");
                    x = true;
                    break;

                case ConsoleKey.G:
                    FlightGenerator.GenerateFlights();
                    break;
                
                case ConsoleKey.S:
                    AdminPanel.Admin();
                    break;
                    
                default:
                    // Checks for capslock/numlock
                    if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    {
                        if (Console.CapsLock && Console.NumberLock)
                        {
                            Console.WriteLine(key.KeyChar);
                            Console.Write("Invalid option");
                        }
                    }
                    break;
            }
        }
    }
}
