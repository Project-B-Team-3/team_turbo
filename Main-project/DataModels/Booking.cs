using Main_project.DataAccess;

namespace Main_project.DataModels;

public class Booking
{
	public string FlightNumber { get; set; }
	public int ReservationNumber { get; set; }
	public List<string> Seats { get; set; }

	public Booking(string flightNumber, string reservationNumber, List<string> seats)
	{
		FlightNumber = flightNumber;
		ReservationNumber = BookingDataAccess.GetBookings().Any() ? BookingDataAccess.GetBookings().Max(h => h.ReservationNumber) + 1 : 1;
		Seats = seats;
	}
}