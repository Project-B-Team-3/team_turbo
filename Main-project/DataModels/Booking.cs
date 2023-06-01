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
	
	public string[] GetLines()
	{
		Flight flight = FlightDataAccess.GetFlights().First(h => h.FlightNumber == FlightNumber);
		string[] lines = {$"You just booked a flight from {flight.DepartureCity} to {flight.DestinationCity}.",
			"Booking Details",
			$"Reservation Number: {ReservationNumber}",
			$"Flight Number: {FlightNumber}",
			$"Departure Airport Code: {flight.DepartureAirportCode}",
			$"Destination Airport Code: {flight.DestinationAirportCode}",
			$"Departure Time: {flight.DepartureTime:dd-M-yyy HH:mm:ss}",
			"",
			"And your seats are:",
			Seats.Values.ToArray().ToString()};
		return lines;
	}
}