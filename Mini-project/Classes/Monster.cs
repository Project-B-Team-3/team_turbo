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

    public Monster(int id, string name, string nameplural, int maximumdamage, int minimumdamage, int rewardexperience, int rewardgold, int currenthitpoints)
    {
        this.Id = id;
        this.Name = name;
        this.NamePlural = nameplural;
        this.MaximumDamage = maximumdamage;
        this.MinimumDamage = minimumdamage;
        this.RewardExperience = rewardexperience;
        this.RewardGold = rewardgold;
        this.CurrentHitPoints = currenthitpoints;

        this.Loot = new CountedItemList();
    }

}