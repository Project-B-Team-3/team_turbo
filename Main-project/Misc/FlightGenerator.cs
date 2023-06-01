using Main_project.DataAccess;
using Main_project.DataModels;

namespace Main_project.Misc;

public class FlightGenerator
{
	public static void GenerateFlights()
	{
		Console.WriteLine("Generating flights...");
		var flightNumberCounter = 1;
		List<Flight> flights = new();
		var rand = new Random();
		var daysToGenerate = 90; // Generate flights for 90 days
		var airplane = AirplaneDataAccess.GetPlanes().First(h => h is { Brand: "Boeing", Model: "737-700" });

		// Airport locations stored in a dictionary
		Dictionary<string, string> airportCodes = new()
		{
			// Europe
			{ "RTM", "Rotterdam" },
			{ "LHR", "London" }, { "CDG", "Paris" }, { "FRA", "Frankfurt" }, { "BCN", "Barcelona" },
			{ "AMS", "Amsterdam" },
			{ "CPH", "Copenhagen" }, { "MAD", "Madrid" }, { "IST", "Istanbul" }, { "ATH", "Athens" },
			{ "ZRH", "Zurich" },
			// America
			{ "JFK", "New York" }, { "LAX", "Los Angeles" }, { "ORD", "Chicago" }, { "SFO", "San Francisco" },
			{ "MCO", "Orlando" },
			{ "DEN", "Denver" }, { "SEA", "Seattle" }, { "MIA", "Miami" }, { "LAS", "Las Vegas" }, { "ATL", "Atlanta" },
			// Asia
			{ "HND", "Tokyo" }, { "NRT", "Tokyo" }, { "PEK", "Beijing" }, { "HKG", "Hong Kong" }, { "DXB", "Dubai" },
			{ "BKK", "Bangkok" }, { "ICN", "Seoul" }, { "KUL", "Kuala Lumpur" }, { "SGN", "Ho Chi Minh City" },
			{ "DEL", "New Delhi" }
		};

		// Generating the next month of flights
		var startDate = DateTime.Today;
		var endDate = startDate + TimeSpan.FromDays(daysToGenerate);

		// Loop through all pairs departpure and destination airports
		foreach (var departureAirport in airportCodes)
		foreach (var destinationAirport in airportCodes)
			// Check if departure and destination airports are different
			if (departureAirport.Key != destinationAirport.Key &&
			    (departureAirport.Key == "RTM" || destinationAirport.Key == "RTM"))
				// Loop through all days in specified time
				for (var date = startDate; date <= endDate; date = date.AddDays(1))
				{
					var minDepartureTime = new TimeSpan(6, 0, 0);
					var maxDepartureTime = new TimeSpan(23, 0, 0);
					var departureDateTime = new DateTime(date.Year, date.Month, date.Day, rand.Next(0, 24), 0, 0);
					if (departureDateTime.TimeOfDay >= minDepartureTime &&
					    departureDateTime.TimeOfDay < maxDepartureTime)
					{
						var priceMin = 50;
						var priceMax = 150;
						if (new[] { "RTM", "LHR", "CDG", "FRA", "BCN", "AMS", "CPH" }.Contains(departureAirport.Key) &&
						    new[] { "RTM", "LHR", "CDG", "FRA", "BCN", "AMS", "CPH" }.Contains(destinationAirport.Key))
						{
							priceMin = 50;
							priceMax = 75;
						}

						var flight = new Flight
						(
							$"ROT{flightNumberCounter:D3}", //:D3 formats int as string with at least 3 digits
							departureAirport.Key,
							destinationAirport.Key,
							departureDateTime,
							airplane.Rows,
							airplane.EconomySeat,
							airplane.BusinessSeat,
							airplane.FirstSeat,
							rand.Next(priceMin, priceMax),
							departureAirport.Value,
							destinationAirport.Value
						);
						flights.Add(flight);
						flightNumberCounter++;
					}
				}

		FlightDataAccess.CreateFlights(flights);
		Console.WriteLine("Finished generating flights!");
		Thread.Sleep(3000);
	}
}