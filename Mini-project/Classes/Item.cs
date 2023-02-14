namespace Mini_project.Classes;

public class Item
{

    public int Id;
    public string Name;
    public string NamePlural;

    public Item(int id, string name, string nameplural)
    {
        this.Id = id;
        this.Name = name;
        this.NamePlural = nameplural;
    }
	
}