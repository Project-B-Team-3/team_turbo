using Main_project.DataModels;
using Main_project.DataAccess;

namespace Main_project.Logic;

public static class FlightLogic
{
	public static List<Flight> FilterFlightsByDate(List<Flight> flights, DateTime travelDate)
	{
		return flights.Where(flight => flight.DepartureTime.Date == travelDate.Date).ToList();
	}

	public static List<Flight> GetFlightsByDestinationAndDate(
		string destination,
		DateTime travelDate
	)
	{
		var allFlights = FlightDataAccess.GetFlights();
		var filteredFlights = allFlights
			.Where(
				flight =>
					flight.DestinationCity.Equals(destination, StringComparison.OrdinalIgnoreCase)
					&& flight.DepartureTime.Date == travelDate.Date
			)
			.ToList();

		return filteredFlights;
	}

	public static decimal CalculateTotalPrice(Flight flight, int numberOfPassengers)
	{
		return flight.Price * numberOfPassengers;
	}
}