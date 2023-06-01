using Main_project.DataAccess;

namespace Main_project.DataModels;

public class Booking
{
	public string ReservationNumber { get; set; }
	public string FlightNumber { get; set; }
	public Dictionary<string, Person> Seats { get; set; }
	public Cost Cost { get; set; }

	public Booking(
		string reservationNumber,
		string flightNumber,
		Dictionary<string, Person> seats,
		Cost cost
	)
	{
		ReservationNumber = reservationNumber;
		FlightNumber = flightNumber;
		Seats = seats;
		Cost = cost;
	}
	
	public List<string> GetLines()
	{
		Flight flight = FlightDataAccess.GetFlights().First(h => h.FlightNumber == FlightNumber);
		string[] lines = {$"You just booked a flight from {flight.DepartureCity} to {flight.DestinationCity}.",
			"Booking Details",
			$"Reservation Number: {ReservationNumber}",
			$"Flight Number: {FlightNumber}",
			$"Departure Airport Code: {flight.DepartureAirportCode}",
			$"Destination Airport Code: {flight.DestinationAirportCode}",
			$"Departure Time: {flight.DepartureTime:dd-M-yyy HH:mm:ss}",
			$"For the total price of: {Cost.GetTotal()}",
			"",
			"And your seats are:"};
		var returnVal = lines.ToList();
		foreach (var person in Seats.Values.ToArray())
		{
			returnVal.Add(person.ToString());
		}
		return returnVal;
	}
}