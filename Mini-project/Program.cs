﻿using Mini_project.Classes;

namespace Mini_project;

public class Program
{
    private static readonly Player ThePlayer = new ("Placeholder", 100, 100, 0, 0, 1,
        World.WeaponByID(0), World.Locations[0], new QuestList(), new CountedItemList());
    public static void Main()
    {
        Console.WriteLine("What do you want your name to be?");

        ThePlayer.Name = Console.ReadLine()!;
        
        if (ThePlayer.Name == "")
        {
            ThePlayer.Name = "The chosen one";
        }
        var gameRunning = true;
        while (gameRunning)
        {
            Console.WriteLine("What would you like to do? \n[S] See stats\n[M] Move\n[F] Fight\n[Q] Quit");
            var choice = Console.ReadLine()!.ToUpper();
            switch (choice)
            {
                case "S":
                    Console.WriteLine(ThePlayer.ToString());
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
                    if (ThePlayer.CurrentLocation.MonsterLivingHere != null)
                    {
                        Console.WriteLine($"You have encountered a {ThePlayer.CurrentLocation.MonsterLivingHere.Name}");
                        Console.WriteLine($"You currently have {ThePlayer.CurrentHitPoints} HP");
                        Console.WriteLine("What do you wish to do?\n[F] Fight the animal \n[R] Run ");
                        var combatchoice = Console.ReadLine()!.ToUpper();
                        switch (combatchoice)
                        {
                            case "F":
                                ThePlayer.CurrentLocation.MonsterLivingHere.Attack();
                                Console.WriteLine($"You have attacked the {ThePlayer.CurrentLocation.MonsterLivingHere.Name} and dealt 100 Damage");
                                if (ThePlayer.CurrentLocation.MonsterLivingHere.CurrentHitPoints <= 0) {
                                    Console.WriteLine($"You killed {ThePlayer.CurrentLocation.MonsterLivingHere.Name}!");
                                    switch(ThePlayer.CurrentLocation.MonsterLivingHere.Name) {
                                        case "rat":
                                            Console.Write($"You have obtained a Rat tail\n");
                                            ThePlayer.Inventory.AddItem(World.ItemByID(1));
                                            break;
                                        case "snake":
                                            Console.Write($"You have obtained a Snake fang\n");
                                            ThePlayer.Inventory.AddItem(World.ItemByID(3));
                                            break;
                                        case "giant spider":
                                            Console.Write($"You have obtained a Spider silk\n");
                                            ThePlayer.Inventory.AddItem(World.ItemByID(6));
                                            break;
                                    }
                                }

                                break;

                            case "R":
                                Console.WriteLine($"You ran away from the {ThePlayer.CurrentLocation.MonsterLivingHere.Name}");
                                break;
                        }
                        


                        
                    }
                    else
                    {
                        Console.WriteLine("There's no monster to fight!");
                    }
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
                MoveToLocation(ThePlayer.CurrentLocation.LocationToNorth);
                break;
            case 2:
                MoveToLocation(ThePlayer.CurrentLocation.LocationToEast);
                break;
            case 3:
                MoveToLocation(ThePlayer.CurrentLocation.LocationToSouth);
                break;
            case 4:
                MoveToLocation(ThePlayer.CurrentLocation.LocationToWest);
                break;
            case 5:
                return;
        }
    }

    private static void MoveToLocation(Location? location)
    {
        if (location == null ||
            ThePlayer.Inventory.TheCountedItemList.Any(h => h.TheItem == location.ItemRequiredToEnter))
        {
            Console.WriteLine("You cannot move in that direction.");
        }
        else
        {
            ThePlayer.CurrentLocation = location;
            Console.WriteLine("You have moved to " + location.Name);
            Console.WriteLine(location.Description);
            if (location.QuestAvailableHere == null || ThePlayer.HasQuest(location.QuestAvailableHere)) return;
            Console.WriteLine($"Do you want to pickup the {location.QuestAvailableHere.Name} quest?");
            if (Console.ReadLine()!.ToLower() == "yes")
            {
                ThePlayer.QuestLog.QuestLog.Add(new PlayerQuest(location.QuestAvailableHere));
                Console.WriteLine("Picked up the quest!");
            }
            else
            {
                Console.WriteLine("Didn't pick up the quest.");
            }
        }
    }
}