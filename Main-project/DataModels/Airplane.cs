namespace Main_project.DataModels;

public class Airplane
{
	public string Brand { get; set; }
	public string Model { get; set; }
	public int EconomySeat { get; set; }
	public int BusinessSeat { get; set; }
	public int FirstSeat { get; set; }

	public Airplane(string brand, string model, int economySeat, int businessSeat, int firstSeat)
	{
		Brand = brand;
		Model = model;
		EconomySeat = economySeat;
		BusinessSeat = businessSeat;
		FirstSeat = firstSeat;
	}
}