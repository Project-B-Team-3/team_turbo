namespace Mini_project;

public class Weapon
{

    public int Id;
    public string Name;
    public string NamePlural;
    public int MinimumDamage;
    public int MaximumDamage;

    public Weapon(int id, string name, string nameplural, int minimumdamage, int maximumdamage)
    {
        this.Id = id;
        this.Name = name;
        this.NamePlural = nameplural;
        this.MinimumDamage = minimumdamage;
        this.MaximumDamage = maximumdamage;
    }
	
}