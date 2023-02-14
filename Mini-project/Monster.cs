namespace Mini_project;

public class Monster
{

    public int Id;
    public string Name;
    public string NamePlural;
    public int MaximumDamage;
    public int RewardExperience;
    public int RewardGold;
    public CountedItemList Loot;
    public int CurrentHitPoints;

    public Monster(int id, string name, string nameplural, int maximumdamage, int rewardexperience, int rewardgold)
    {
        this.Id = id;
        this.Name = name;
        this.NamePlural = nameplural;
        this.MaximumDamage = maximumdamage;
        this.RewardExperience = rewardexperience;
        this.RewardGold = rewardgold;
        this.Loot = new CountedItemList();
    }

}