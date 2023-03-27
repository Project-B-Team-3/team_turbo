namespace Main_project.Presentation;

public static class AdminPanel
{
	public static void Admin()
	{
		Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Admin Panel");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"What do you wish to do?\n[1] " +
            $"View all available flights\n[2] Change seat pricing \n[3] Change catering pricing\n[4] Change catering items\n" +
            $"[5] Change Ticket pricing\n[6] Go back to main panel");
        ConsoleKeyInfo Adminchoice;
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
                break;

            default:
                // Checks for capslock/numlock
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    if (Console.CapsLock && Console.NumberLock)
                    {
                        Console.WriteLine(Adminchoice.KeyChar);
                        Console.Write("Invalid option");
                    }
                }
                break;
        }
	}
}