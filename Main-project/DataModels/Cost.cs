namespace Main_project.DataModels;

public class Cost
{
	public decimal FlightPrice { get; set; }
	public decimal Catering { get; set; }
	public List<int> SeatPrices { get; set; }

	public Cost(int flightPrice, int catering, List<int> seatPrices)
	{
		FlightPrice = flightPrice;
		Catering = catering;
		SeatPrices = seatPrices;
	}
}