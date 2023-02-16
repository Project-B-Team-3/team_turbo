using Mini_project.Classes;

namespace Mini_project;

public class Program
{
    private static Player _player = new ("chosen one", 100, 100, 0, 0, 1,
        World.WeaponByID(0), World.Locations[0], new QuestList(), new CountedItemList());
    public static void Main()
    {
        var gameRunning = true;
        while (gameRunning)
        {
            Console.WriteLine("What would you like to do? \n[S] See stats\n[M] Move\n[F] Fight\n[Q] Quit");
            var choice = Console.ReadLine()!.ToUpper();
            switch (choice)
            {
                case "S":
                    //TODO show stats
                    break;
                case "M":
                    Console.WriteLine("What direction do you want to move in?");
                    Console.WriteLine("1. Move North");
                    Console.WriteLine("2. Move East");
                    Console.WriteLine("3. Move South");
                    Console.WriteLine("4. Move West");
                    Console.WriteLine("5. Go back");
                    ActionsMenu(int.Parse(Console.ReadLine()!));
                    break;
                case "F":
                    //TODO open fight dialog
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
    private static void ActionsMenu(int actions)
    {
        switch (actions)
        {
            case 1:
                MoveToLocation(_player.CurrentLocation.LocationToNorth);
                break;
            case 2:
                MoveToLocation(_player.CurrentLocation.LocationToEast);
                break;
            case 3:
                MoveToLocation(_player.CurrentLocation.LocationToSouth);
                break;
            case 4:
                MoveToLocation(_player.CurrentLocation.LocationToWest);
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
}