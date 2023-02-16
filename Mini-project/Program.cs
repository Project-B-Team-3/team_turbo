using Mini_project.Classes;

namespace Mini_project;

public class Program
{
    private static Player _player = new Player("chosen one", 100, 100, 0, 0, 1,
        World.WeaponByID(0), World.Locations[0], new QuestList(), new CountedItemList());
    public static void Main()
    {
        Console.WriteLine("1. Move North");
        Console.WriteLine("2. Move East");
        Console.WriteLine("3. Move South");
        Console.WriteLine("4. Move West");
        Console.WriteLine("5. Load game");
        Console.WriteLine("6. Quit");
    }
    private static void ActionsMenu(int actions)
    {
        switch (actions)
        {
            case 1:
                if (_player.CurrentLocation.LocationToNorth != null)
                {
                    MoveToLocation(_player.CurrentLocation.LocationToNorth);
                }
                else
                {
                    Console.WriteLine("No location to the north");
                }
                break;
            case 2:
                if (_player.CurrentLocation.LocationToEast != null)
                {
                    MoveToLocation(_player.CurrentLocation.LocationToEast);
                }
                else
                {
                    Console.WriteLine("No location to the east");
                }
                break;
            case 3:
                if (_player.CurrentLocation.LocationToSouth != null)
                {
                    MoveToLocation(_player.CurrentLocation.LocationToSouth);
                }
                else
                {
                    Console.WriteLine("No location to the south");
                }
                break;
            case 4:
                if (_player.CurrentLocation.LocationToWest != null)
                {
                    MoveToLocation(_player.CurrentLocation.LocationToWest);
                }
                else
                {
                    Console.WriteLine("No location to the west");
                }
                break;
            case 5:
                return;
        }
    }

    private static void MoveToLocation(Location? location)
    {
        if (location == null)
        {
            Console.WriteLine("You cannot move in that direction.");
        }
        else
        {
            _player.CurrentLocation = location;
            Console.WriteLine("You have moved to " + location.Name);
            Console.WriteLine(location.Description);
            
            // if (location.QuestAvailableHere != null && !Player.HasQuest(location.QuestAvailableHere))
            // {
            //
            // }
            // if (location.MonsterLivingHere != null)
            // {
            //
            // }
        }
    }
    private static void Quit()
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