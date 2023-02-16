using Mini_project.Classes;

namespace Mini_project;

public class Program {
    public static void DisplayMenu()
    {
        Console.WriteLine("1. Move North");
        Console.WriteLine("2. Move East");
        Console.WriteLine("3. Move South");
        Console.WriteLine("4. Move West");
        Console.WriteLine("5. Load game");
        Console.WriteLine("6. Quit");
    }
    public static void ActionsMenu(int actions)
    {
        switch (actions)
        {
            case 1: 
                break;
            case 2: 
                break;    
            case 3: 
                break;    
            case 4: 
                break;    
        }

    }
    public static void MoveToLocation(Location location)
    {
        if (location == null)
        {
            Console.WriteLine("You cannot move in that direction.");
        }
        else
        {
            Player.CurrentLocation = location;
            Console.WriteLine("You have moved to " + location.Name);
            Console.WriteLine(location.Description);
            
            if (location.QuestAvailableHere != null && !Player.HasQuest(location.QuestAvailableHere))
            {

            }
            if (location.MonsterLivingHere != null)
            {

            }
        }
    }
    public static Player Player1 { get; set; }

    static void Main(string[] args)
    {
        Player1 = new Player("chosen one", 100, 100, 0, 0, 1, null, World.Locations[0], 0, 0);
    }
    public static void Quit()
    {
        var gameRunning = true;
        while (gameRunning)
        {
            Console.WriteLine("What would you like to do? \n[Q] Quit\n[?] todo");
            var choice = Console.ReadLine()!.ToUpper();
            switch (choice)
            {
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