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