namespace Mini_project;

class Program
{
	public static void Main()
	{
		bool GameRunning = true;
		while (GameRunning == true) 
		{
			Console.WriteLine("What would you like to do? \n[Q] Quit\n[?] todo");
			string choice = Console.ReadLine();
			if (choice == "Q")
			{
				Console.WriteLine("Game has been quit");
				GameRunning = false;
			}
			else
			{
				Console.WriteLine("Invalid option, Make sure to choose from the above.");
			}
		}
		
	}
}