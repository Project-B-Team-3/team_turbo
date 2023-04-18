namespace Main_project.DataModels;

public class Booking
{
	public string FlightNumber { get; set; }
	public string ReservationNumber { get; set; }
	public List<string> Seats { get; set; }
}