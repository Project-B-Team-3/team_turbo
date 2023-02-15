namespace Mini_project;

internal class Program
{
	public static void Main()
	{
		var gameRunning = true;
		while (gameRunning)
		{
			Console.WriteLine("What would you like to do? \n[Q] Quit\n[?] todo");
			var choice = Console.ReadLine()?.ToUpper();
			switch (choice)
			{
				case null:
					throw new Exception("choice can't be null");
				case "Q":
					Console.WriteLine("Game has been quit");
					gameRunning = false;
					break;
				default:
					Console.WriteLine("Invalid option, Make sure to choose from the above.");
					break;
			}
		}
	}
}