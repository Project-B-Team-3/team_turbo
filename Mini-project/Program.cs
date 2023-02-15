namespace Mini_project;

internal class Program
{
	public static void Main()
	{
		var gameRunning = true;
		while (gameRunning)
		{
			var sAdventure = new SuperAdventure();
			Console.WriteLine("What would you like to do? \n[Q] Quit\n[?] todo");
			var choice = Console.ReadLine()!.ToUpper();
			switch (choice)
			{
				case "M":
					Console.WriteLine($"You are currently at {sAdventure.ThePlayer.CurrentLocation})");
					//move
					break;
				case "F":
					//fight
					break;
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