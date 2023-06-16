using Main_project.DataAccess;
using Main_project.Logic;

namespace Main_project.Presentation;

public static class ConsoleView
{
	public static void DisplayFlights()
	{
		Console.Clear();
		Console.WriteLine(
			"Please enter the desired destination to view all upcoming flights:"
		);
		var destination = Console.ReadLine()?.ToUpper();

		var flights = FlightDataAccess.GetFlightsByDestination(destination);

		if (flights.Count == 0)
		{
			Console.WriteLine("No flights available.");
		}
		else
		{
			Console.WriteLine("Upcoming flights:");


			for (var i = 0; i < flights.Count; i++)
			{
				var flight = flights[i];
				Console.WriteLine($"{i + 1}. {flight}");
			}
		}

		Console.WriteLine("\nPress any key to continue...");
		Console.ReadKey(true);
	}

	public static void DisplayAllFlights()
	{
		var allFlights = BookingLogic.GetAllFlights();

		Console.WriteLine("All Flights:");
		Console.WriteLine("-------------");
		foreach (var flight in allFlights)
		{
			Console.WriteLine($"Flight number: {flight.FlightNumber}");
			Console.WriteLine($"Departure: {flight.DepartureCity}");
			Console.WriteLine($"Destination: {flight.DestinationCity}");
			Console.WriteLine($"Departure time: {flight.DepartureTime}");
			Console.WriteLine($"Seats available: {flight.Seats.Count}");
			Console.WriteLine($"Price: {flight.Price}");
			Console.WriteLine();
		}

		Console.WriteLine("Press any key to continue...");
		Console.ReadKey();
	}

	public static void SelectFlight()
	{
		var destination = GetDestinationFromUser();

		var matchingFlights = BookingLogic
			.UpComingFlights()
			.Where(
				flight =>
					flight.DestinationCity.Equals(
						destination,
						StringComparison.OrdinalIgnoreCase
					)
			)
			.ToList();

		if (!matchingFlights.Any())
		{
			Console.WriteLine($"No upcoming flights to {destination}!");
			Console.ReadKey();
			return;
		}

		Console.WriteLine($"Upcoming Flights to {destination}:");
		Console.WriteLine("-----------------------------------");

		for (var i = 0; i < matchingFlights.Count(); i++)
		{
			var flight = matchingFlights.ElementAt(i);
			Console.WriteLine(
				$"{i + 1}: Flight {flight.FlightNumber} - {flight.DepartureCity} to {flight.DestinationCity}, Departure: {flight.DepartureTime}"
			);
		}

		Console.Write("Enter the number of the flight you want to select: ");
		var input = Console.ReadLine();

		if (
			int.TryParse(input, out var index) && index >= 1 && index <= matchingFlights.Count()
		)
		{
			var selectedFlight = FlightDataAccess.GetFlightByNumber(
				matchingFlights.ElementAt(index - 1).FlightNumber
			);

			if (selectedFlight != null)
			{
				Console.WriteLine();
				Console.WriteLine($"Selected flight: {selectedFlight.FlightNumber}");
			}
			else
			{
				Console.WriteLine("Invalid input. Please enter a valid flight number.");
			}
		}
		else
		{
			Console.WriteLine("Invalid input. Please enter a valid flight number.");
		}

		Console.WriteLine();
		Console.WriteLine("Press any key to continue...");
		Console.ReadKey();
	}

	private static string GetDestinationFromUser()
	{
		Console.Write("Enter your destination city: ");
		return Console.ReadLine();
	}
}