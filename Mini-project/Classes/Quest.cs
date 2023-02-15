namespace Mini_project.Classes;

public class Quest
{
	public int Id;
	public string Name;
	public string Description;
	public int RewardExperiencePoints;
	public int RewardGold;
	public Item? RewardItem;
	public Weapon? RewardWeapon;
	public CountedItemList QuestCompletionItems;

	public Quest(int id, string name, string description, int rewardExp, int rewardGold, Item? rewardItem,
		Weapon? rewardWeapon)
	{
		Id = id;
		Name = name;
		Description = description;
		RewardExperiencePoints = rewardExp;
		RewardGold = rewardGold;
		RewardItem = rewardItem;
		RewardWeapon = rewardWeapon;
		QuestCompletionItems = new CountedItemList();
	}
}