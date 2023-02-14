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

    public Quest(int id, string name, string description, int rewardExp, int rewardGold, Item? rewardItem, Weapon? rewardWeapon)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.RewardExperiencePoints = rewardExp;
        this.RewardGold = rewardGold;
        this.RewardItem = rewardItem;
        this.RewardWeapon = rewardWeapon;
        this.QuestCompletionItems = new CountedItemList();
    }

}
