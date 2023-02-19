namespace Mini_project.Classes;

public class Monster
{
    public int Id;
    public string Name;
    public string NamePlural;
    public int MaximumDamage;
    public int MinimumDamage;
    public int? RewardExperience;
    public int? RewardGold;
    public CountedItemList Loot;
    public int CurrentHitPoints;
    private Random _random = new Random();

    public Monster(int id, string name, string nameplural, int maximumdamage, int minimumdamage, int rewardexperience,
        int rewardgold, int currenthitpoints)
    {
        Id = id;
        Name = name;
        NamePlural = nameplural;
        MaximumDamage = maximumdamage;
        MinimumDamage = minimumdamage;
        RewardExperience = rewardexperience;
        RewardGold = rewardgold;
        CurrentHitPoints = currenthitpoints;

        Loot = new CountedItemList();
    }

    public int Attack()
    {
        return _random.Next(MaximumDamage + 1);
    }
}
