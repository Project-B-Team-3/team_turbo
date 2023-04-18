using Main_project.DataAccess;

namespace Main_project.DataModels;

public class Booking
{
	public int ReservationNumber { get; }
	public string FlightNumber { get; }
	public Dictionary<string, Person> Seats { get; set; }

	public Booking(string flightNumber, Dictionary<string, Person> seats)
	{
		ReservationNumber = BookingDataAccess.GetBookings().Any() ? BookingDataAccess.GetBookings().Max(h => h.ReservationNumber) + 1 : 1;
		FlightNumber = flightNumber;
		Seats = seats;
	}
}