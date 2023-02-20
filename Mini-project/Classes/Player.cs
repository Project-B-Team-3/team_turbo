namespace Mini_project.Classes;

public class Player
{
    public string Name;
    public int CurrentHitPoints;
    public int MaximumHitPoints;
    public int Gold;
    public int ExperiencePoints;
    public int Level;
    public Weapon CurrentWeapon;
    public Location CurrentLocation;
    public QuestList QuestLog;
    public CountedItemList Inventory;
    private Random _random = new Random();

    public Player(string name, int currentHitPoints, int maximumHitPoints, int gold, int experiencePoints, int level, Weapon currentWeapon, Location currentLocation, QuestList questLog, CountedItemList inventory)
    {
        Name = name;
        CurrentHitPoints = currentHitPoints;
        MaximumHitPoints = maximumHitPoints;
        Gold = gold;
        ExperiencePoints = experiencePoints;
        Level = level;
        CurrentWeapon = currentWeapon;
        CurrentLocation = currentLocation;
        QuestLog = questLog;
        Inventory = inventory;
    }

	  public bool HasQuest(Quest quest)
	  {
		  return QuestLog.QuestLog.Any(h => h.TheQuest == quest);
	  }

    public override string ToString()
    {
        return $"Your name is {Name} and you have {CurrentHitPoints} lives left!";
    }

    public void Heal(int amount)
    {
        CurrentHitPoints += amount;

        if (CurrentHitPoints > MaximumHitPoints)
        {
            CurrentHitPoints = MaximumHitPoints;
        }
    }

    public int Attack()
    {
        return _random.Next(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage + 1);
    }

    public void GoToHouse()
    {
        CurrentHitPoints = MaximumHitPoints;
        Console.WriteLine("You have returned home and regained your health.");
    }

    public void Fight(Monster monster)
    {
        Console.WriteLine($"You are fighting a {monster.Name}!");

        while (CurrentHitPoints > 0 && monster.CurrentHitPoints > 0)
        {
            Console.WriteLine($"Your hit points: {CurrentHitPoints} | {monster.Name}'s hit points: {monster.CurrentHitPoints}");
            Console.WriteLine("Choose an action:");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Flee");
            Console.WriteLine("3. Use Healing Potion");

            string input = Console.ReadLine()!;
            if (input == "1")
            {
                int monsterRemainingHitPoints = monster.CurrentHitPoints - Attack();

                if (monsterRemainingHitPoints <= 0)
                {
                    Console.WriteLine($"You have defeated the {monster.Name}!");
                    Gold += monster.RewardGold ?? 0;
                    ExperiencePoints += monster.RewardExperience ?? 0;
                    Inventory.AddItem(monster.Loot);
                    LevelUpCheck();
                    return;
                }

                int playerRemainingHitPoints = CurrentHitPoints - monster.Attack();

                if (playerRemainingHitPoints <= 0)
                {
                    Console.WriteLine($"You have been defeated by the {monster.Name}!");
                    GoToHouse();

                    // Get a list of all the items in the inventory, excluding the Adventurer's Pass
                    List<Item> itemsToRemove = Inventory.Items.Where(item => item.Name != "Adventurer's Pass").ToList();

                    if (itemsToRemove.Count > 0)
                    {
                        // Randomly select an item from the list of items to remove
                        int indexToRemove = _random.Next(0, itemsToRemove.Count);

                        // Remove the selected item from the inventory
                        Inventory.RemoveItem(indexToRemove);
                        Console.WriteLine($"The {monster.Name} took your {itemsToRemove[indexToRemove].Name}!");
                    }
                    else
                    {
                        Console.WriteLine("The monster didn't find anything of value to take from you.");
                    }

                    return;
                }

                monster.CurrentHitPoints = monsterRemainingHitPoints;
                CurrentHitPoints = playerRemainingHitPoints;
            }
            else if (input == "2")
            {
                Console.WriteLine("You flee from the fight!");
                return;
            }
            else if (input == "3")
            {
                List<Item> healingPotions = Inventory.Items.Where(item => item is HealingPotion).ToList();

                if (healingPotions.Count > 0)
                {
                    Console.WriteLine("Select a healing potion to use:");

                    for (int i = 0; i < healingPotions.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {healingPotions[i].Name}");
                    }

                    string potionInput = Console.ReadLine()!;
                    if (int.TryParse(potionInput, out int potionIndex) && potionIndex > 0 && potionIndex <= healingPotions.Count)
                    {
                        HealingPotion potion = (HealingPotion)healingPotions[potionIndex - 1];
                        Inventory.RemoveItem(potion);
                        Heal(potion.Amount);
                    }
                }
            }
        }
    }

    public void LevelUpCheck()
    {
        int experienceNeededForLevelUp = 100 * Level;

        if (ExperiencePoints >= experienceNeededForLevelUp)
        {
            Level++;
            Console.WriteLine($"Congratulations! You've reached level {Level}!");
            ExperiencePoints -= experienceNeededForLevelUp;

            // Increase the player's maximum hit points and restore their current hit points to full
            MaximumHitPoints += 10;
            CurrentHitPoints = MaximumHitPoints;

            // Increase the player's attack power by 1
            CurrentWeapon.MaximumDamage++;

            // Inform the player of their new stats
            Console.WriteLine($"Your maximum hit points have increased to {MaximumHitPoints} and your attack power has increased to {CurrentWeapon.MaximumDamage}!");
        }
    }
}
