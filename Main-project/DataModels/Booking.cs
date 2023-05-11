namespace Main_project.DataModels;

public class Booking
{
	public string ReservationNumber { get; }
	public string FlightNumber { get; }
	public Dictionary<string, Person> Seats { get; set; }

	public Booking(string reservationNumber, string flightNumber, Dictionary<string, Person> seats)
	{
		ReservationNumber = reservationNumber;
		FlightNumber = flightNumber;
		Seats = seats;
	}
}