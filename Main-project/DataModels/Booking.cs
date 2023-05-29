namespace Main_project.DataModels;

public class Booking
{
	public string ReservationNumber { get; }
	public string FlightNumber { get; }
	public Dictionary<string, Person> Seats { get; set; }
	public Cost Cost { get; set; }

	public Booking(string reservationNumber, string flightNumber, Dictionary<string, Person> seats, Cost cost)
	{
		ReservationNumber = reservationNumber;
		FlightNumber = flightNumber;
		Seats = seats;
		Cost = cost;
	}
}