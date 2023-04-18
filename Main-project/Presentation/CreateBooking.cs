using Main_project.DataAccess;
using Main_project.DataModels;

namespace Main_project.Presentation;

public static class CreateBooking
{
	public static void CreateNewBooking()
	{
		Console.Clear();
		Console.WriteLine("Welcome to the booking menu, please enter a flight number to book a flight.");
		var flightNum = Console.ReadLine();
		Console.WriteLine("For how many people would you like to book?");
		var peopleCount = Console.ReadLine();
		var peopleInt = 0;
		if (flightNum is null || peopleCount is null ||
		    flightNum == "" || peopleCount == "" || !int.TryParse(peopleCount, out peopleInt))
		{
			Console.WriteLine("Wrong input...");
			return;
		}

		Dictionary<string, Person> seats = new();
		for (int i = 0; i < peopleInt; i++)
		{
			Console.WriteLine($"What is the name of person #{i}?");
			var name = Console.ReadLine();
			Console.WriteLine("What is the birthdate of this person?");
			var birthdate = Console.ReadLine();
			Console.WriteLine("What is the ID, driver's license or passport document number of this person?");
			var docNum = Console.ReadLine();
			if (name is null || birthdate is null || docNum is null ||
			    name == "" || birthdate == "" || docNum == "")
			{
				Console.WriteLine("Invalid data entered, please try again...");
				Thread.Sleep(200);
				Console.Clear();
				i--;
				continue;
			}

			var seat = SeatSelector.SelectSeat(flightNum);
			seats.Add(seat, new Person(name, birthdate, docNum));
			Console.WriteLine("Successfully added another person to the booking.");
			Thread.Sleep(200);
		}
		
		BookingDataAccess.CreateBooking(new Booking(flightNum, seats));
	}
}