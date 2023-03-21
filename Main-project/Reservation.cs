namespace Main_project;

public class Reservation
{
	private Flight _flight;
	private List<Person> _person;

	public Reservation(Flight flight, List<Person> person)
	{
		_flight = flight;
		_person = person;
	}
}