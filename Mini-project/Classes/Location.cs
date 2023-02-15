namespace Mini_project.Classes;

public class Location
{
	public int Id;
	public string Name;
	public string Description;
	public Item? ItemRequiredToEnter;
	public Quest? QuestAvailableHere;
	public Monster? MonsterLivingHere;
	public Location? LocationToNorth;
	public Location? LocationToEast;
	public Location? LocationToSouth;
	public Location? LocationToWest;

	public Location(int id, string name, string description, Item? itemrequiredtoenter,
		Quest? questavailablehere, Monster? monsterlivinghere)
	{
		Id = id;
		Name = name;
		Description = description;
		if (itemrequiredtoenter != null) ItemRequiredToEnter = itemrequiredtoenter;
		if (questavailablehere != null) QuestAvailableHere = questavailablehere;
		if (monsterlivinghere != null) MonsterLivingHere = monsterlivinghere;
	}
}