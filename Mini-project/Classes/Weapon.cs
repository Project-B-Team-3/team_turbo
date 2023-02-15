namespace Mini_project.Classes;

public class Weapon
{
	public int Id;
	public string Name;
	public string NamePlural;
	public int MinimumDamage;
	public int MaximumDamage;

	public Weapon(int id, string name, string nameplural, int minimumdamage, int maximumdamage)
	{
		Id = id;
		Name = name;
		NamePlural = nameplural;
		MinimumDamage = minimumdamage;
		MaximumDamage = maximumdamage;
	}
}