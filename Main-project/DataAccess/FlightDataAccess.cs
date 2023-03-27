using Newtonsoft.Json;
using Main_project.DataModels;

namespace Main_project.DataAccess
{
    public class FlightDataAccess
    {
        private readonly string _jsonPath;

        public FlightDataAccess()
        {
            _jsonPath = "./DataSources/Flights.json";
        }

        public List<Flight> GetFlights()
        {
            try
            {
                var reader = new StreamReader(_jsonPath);
                var json = reader.ReadToEnd();
                var flights = JsonConvert.DeserializeObject<List<Flight>>(json)!;
                return flights;
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e);
                Environment.Exit(1);
                return new List<Flight>();
            }
        }

        public void UpdateFlight(Flight flight)
        {
            List<Flight> flights = GetFlights();

            int index = flights.FindIndex(f => f.FlightNumber == flight.FlightNumber);

            if (index != -1)
            {
                flights[index] = flight;

                using (StreamWriter writer = new StreamWriter(_jsonPath))
                {
                    string json = JsonConvert.SerializeObject(flights, Formatting.Indented);
                    writer.Write(json);
                }
            }
        }
    }
}