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
        Quest? questavailablehere, Monster? monsterlivinghere)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        if(itemrequiredtoenter != null) this.ItemRequiredToEnter = itemrequiredtoenter;
        if(questavailablehere != null) this.QuestAvailableHere = questavailablehere;
        if(monsterlivinghere != null) this. MonsterLivingHere = monsterlivinghere;

    }
}