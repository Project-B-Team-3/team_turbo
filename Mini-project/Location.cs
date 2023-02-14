namespace Mini_project;

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
        Quest? questavailablehere, Monster? monsterlivinghere, Location? locationtonorth ,
        Location? locationtoeast, Location? locationtosouth ,Location? locationtowest)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.ItemRequiredToEnter = itemrequiredtoenter;
        this.QuestAvailableHere = questavailablehere;
        this. MonsterLivingHere = monsterlivinghere;
        this.LocationToNorth = locationtonorth;
        this.LocationToEast = locationtoeast;
        this.LocationToSouth = locationtosouth;
        this.LocationToWest = locationtowest;

    }
}