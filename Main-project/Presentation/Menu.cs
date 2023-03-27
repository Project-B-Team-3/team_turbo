namespace Main_project.Presentation
{
    public class Menu
    {
        public static void Start()
        {
            bool adminpanelrunning = false;
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

                    case ConsoleKey.D4: case ConsoleKey.Q:
                        Console.WriteLine("Program has been quit");
                        x = true;
                        break;

                    case ConsoleKey.S:
                        adminpanelrunning = true;
                        break;
                        
                    default:
                        // Checks for capslock/numlock
                        if (Console.CapsLock && Console.NumberLock)
                        {
                            Console.WriteLine(key.KeyChar);

                        }
                        break;
                        {

                        }
                }
                while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape && adminpanelrunning == true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Admin Panel");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"What do you wish to do?\n[1] " +
                        $"View all available flights\n[2] Change seat pricing \n[3] Change catering pricing\n[4] Change catering items\n" +
                        $"[5] Change Ticket pricing\n[6] Go back to main panel");
                    ConsoleKeyInfo Adminchoice = new ConsoleKeyInfo();
                    Adminchoice = Console.ReadKey(true);
                    switch (Adminchoice.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine("Viewing all available flights");
                            break;

                        case ConsoleKey.D2:
                            Console.WriteLine("Change seat pricing");
                            break;

                        case ConsoleKey.D3:
                            Console.WriteLine("Change catering pricing");
                            break;

                        case ConsoleKey.D4:
                            Console.WriteLine("Change catering items");
                            break;

                        case ConsoleKey.D5:
                            Console.WriteLine("Change ticket pricing");
                            break;
                        case ConsoleKey.D6:
                            Console.WriteLine("Program has been quit");
                            adminpanelrunning = false;
                            break;

                        default:
                            // Checks for capslock/numlock
                            if (Console.CapsLock && Console.NumberLock)
                            {
                                Console.WriteLine(key.KeyChar);

                            }
                            break;
                            {

                            }
                    }



                    
                
                }   
            }
            
        }
    }
}
