namespace Main_project.DataModels;

public class Airplane
{
	public string Brand { get; }
	public string Model { get; }
	public int Rows { get; }
	public int EconomySeat { get; }
	public int BusinessSeat { get; }
	public int FirstSeat { get; }

	public Airplane(string brand, string model, int rows, int economySeat, int businessSeat, int firstSeat)
	{
		Brand = brand;
		Model = model;
		Rows = rows;
		EconomySeat = economySeat;
		BusinessSeat = businessSeat;
		FirstSeat = firstSeat;
	}
}