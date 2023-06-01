using Newtonsoft.Json;
using Main_project.DataModels;

namespace Main_project.DataAccess;

public static class FlightDataAccess
{
	public static List<Flight> GetFlights()
	{
		var path = "./DataSources/Flights.json";
		if (!File.Exists(path))
		{
			File.Create(path).Close();
		}

		var json = File.ReadAllText(path);
		var flights = JsonConvert.DeserializeObject<List<Flight>>(json);
		return flights ?? new List<Flight>();
	}

	public static void CreateFlight(Flight flight)
	{
		var flights = GetFlights();
		flights.Add(flight);
		File.WriteAllText("./DataSources/Flights.json", JsonConvert.SerializeObject(flights, Formatting.Indented));
	}

	public static void CreateFlights(List<Flight> flights)
	{
		var newflights = GetFlights();
		newflights = newflights.Concat(flights).ToList();
		File.WriteAllText("./DataSources/Flights.json", JsonConvert.SerializeObject(newflights, Formatting.Indented));
	}

	public static void UpdateFlight(Flight flight)
	{
		List<Flight> flights = GetFlights();

		var index = flights.FindIndex(f => f.FlightNumber == flight.FlightNumber);

		if (index != -1)
		{
			flights[index] = flight;
			File.WriteAllText("./DataSources/Flights.json", JsonConvert.SerializeObject(flights, Formatting.Indented));
		}
	}

	public static void DeleteFlight(Flight flight)
	{
		List<Flight> flights = GetFlights();

		var index = flights.FindIndex(f => f.FlightNumber == flight.FlightNumber);

		if (index != -1)
		{
			flights.RemoveAt(index);
			File.WriteAllText("./DataSources/Flights.json", JsonConvert.SerializeObject(flights, Formatting.Indented));
		}
	}
}