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

	public override string ToString()
	{
		return $"Your name is {Name} and you have {CurrentHitPoints} lives left!";
	}
}

