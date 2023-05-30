namespace Main_project.DataModels;

public class Cost
{
	public decimal FlightPrice { get; set; }
	public decimal Catering { get; set; }
	// still need to add luggage
	public List<decimal> SeatPrices { get; set; }

	public Cost(int flightPrice, int catering, List<decimal> seatPrices)
	{
		FlightPrice = flightPrice;
		Catering = catering;
		SeatPrices = seatPrices;
	}

	public override string ToString()
	{
		return $"The price for the flights is {FlightPrice}\n" +
		       $"The price for the catering is {Catering}\n" +
		       $"The total for all the seats is {SeatPrices.Sum()}\n" +
		       $"The total is: {FlightPrice + Catering + SeatPrices.Sum()}";
	}
}