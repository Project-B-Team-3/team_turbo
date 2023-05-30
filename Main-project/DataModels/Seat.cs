namespace Main_project.DataModels;

public class Seat
{
	public string Number { get; set; }
	public bool Available { get; set; }
	public string Class { get; set; }
	public decimal Price { get; set; }

	public override string ToString()
	{
		return $"Number: {Number}, Available: {Available}, {Class}";
	}
}