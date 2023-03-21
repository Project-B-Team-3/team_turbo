using System.Security.Cryptography;

namespace Main_project;

public class Reservation
{
	private int _number;
	private Flight _flight;
	private List<Person> _person;

	public Reservation(int number, Flight flight, List<Person> person)
	{
		_flight = flight;
		_person = person;
		_number = number;
	}
}