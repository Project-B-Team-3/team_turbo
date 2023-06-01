using System.Text.RegularExpressions;
using Main_project.DataAccess;

namespace Main_project.DataModels;

public class Booking
{
	private string reservationNumber;

	public string ReservationNumber
	{
		get => reservationNumber;
		set
		{
			if (!IsValidReservationCode(value)) throw new ArgumentException("Invalid reservation code.");
			reservationNumber = value;
		}
	}

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
			Seats.Values.ToArray().ToString()};
		return lines;
	}

	private bool IsValidReservationCode(string code)
	{
		if (code.Length < 8)
			return false;

		var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
		var regex = new Regex(pattern);
		return regex.IsMatch(code);
	}
}