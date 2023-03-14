namespace Main_project;

public class Reservation
{
	private Flight _flight;
	private Person _person;

	public Reservation(Flight flight, Person person)
	{
		_flight = flight;
		_person = person;
	}
}