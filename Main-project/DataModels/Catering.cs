namespace Main_project.DataModels;

public class Catering
{
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public bool IsHalal { get; set; }

	public Catering(string name, string description, decimal price, bool isHalal)
	{
		Name = name;
		Description = description;
		Price = price;
		IsHalal = isHalal;
	}
}