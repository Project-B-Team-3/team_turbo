namespace Main_project.DataModels;

public class Cost
{
	public decimal FlightPrice { get; set; }
	public decimal Catering { get; set; }
	public List<decimal> SeatPrices { get; set; }
	public decimal CateringTotalPrice { get; set; }
	public List<Catering> CateringItems { get; set; }

	public Cost(decimal flightPrice, decimal catering, List<decimal> seatPrices)
	{
		FlightPrice = flightPrice;
		Catering = catering;
		SeatPrices = seatPrices;
		CateringTotalPrice = 0M;
		CateringItems = new List<Catering>();
	}

	public decimal GetTotal()
	{
		var totalCateringPrice = CateringItems.Sum(c => c.Price);
		return FlightPrice + Catering + SeatPrices.Sum() + totalCateringPrice;
	}

	public override string ToString()
	{
		var totalCateringPrice = CateringItems.Sum(c => c.Price);
		return $"The price for the flights is {FlightPrice}\n"
		       + $"The price for the catering is {Catering}\n"
		       + $"The total for all the seats is {SeatPrices.Sum()}\n"
		       + $"The total catering price is {totalCateringPrice}\n"
		       + $"The total is: {GetTotal()}";
	}
}