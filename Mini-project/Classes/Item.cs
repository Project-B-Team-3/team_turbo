namespace Mini_project.Classes;

public class Item
{
	public readonly int Id;
	public string Name;
	public string NamePlural;

	public Item(int id, string name, string nameplural)
	{
		Id = id;
		Name = name;
		NamePlural = nameplural;
	}
}

// public static class Items
// {
// 	public static List<Item> PossibleDrops = new List<Item>
// 	{
// 		new Item("Potion", "A small health potion", 10),
// 		new Item("Major Healing Potion", "A major health potion", 50),
// 		new Item("Sword", "A basic sword", 20),
// 		new Item("Shield", "A basic shield", 15),
// 		new Item("Ring of Strength", "A magical ring that enhances the wearer's strength", 50),
// 		new Item("Amulet of Intelligence", "A magical amulet that enhances the wearer's intelligence", 50),
// 		new Item("Wand of Fireballs", "A magical wand that can conjure fireballs", 75),
// 		new Item("Staff of Lightning", "A magical staff that can summon lightning bolts", 75),
// 		new Item("Bow of Accuracy", "A bow that enhances the wearer's accuracy in combat", 50)
// 	};
// }