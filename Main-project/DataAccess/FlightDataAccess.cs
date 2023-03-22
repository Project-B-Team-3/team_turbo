using Newtonsoft.Json;

namespace Main_project
{
    public class FlightDataAccess
    {
        private readonly string _jsonPath;

        public FlightDataAccess(string jsonPath)
        {
            _jsonPath = jsonPath;
        }

        public List<Flight> GetFlights()
        {
            List<Flight> flights;

            using (StreamReader reader = new StreamReader(_jsonPath))
            {
                string json = reader.ReadToEnd();
                flights = JsonConvert.DeserializeObject<List<Flight>>(json);
            }

            return flights;
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