namespace Mini_project;

public class Quest {

    public int Id;
    public string Name;
    public string Description;
    public int RewardExperiencePoints;
    public int RewardGold;
    public Item? RewardItem;
    public Weapon? RewardWeapon;
    public CountedItemList QuestCompletionItems;

    public Quest(int id, string name, string description, int rewardexp, int rewardgold, Item? rewarditem, Weapon? rewardweapon)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.RewardExperiencePoints = rewardexp;
        this.RewardGold = rewardgold;
        this.RewardItem = rewarditem;
        this.RewardWeapon = rewardweapon;
        this.QuestCompletionItems = new CountedItemList();
    }

}
