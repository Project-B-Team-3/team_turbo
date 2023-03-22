using Newtonsoft.Json;

namespace Main_project.Logic
{
    public class BookingLogic
    {
        private const string _jsonPath = "Flight.json";

        public BookingLogic()
        {
        }

        public List<Flight> GetFlights()
        {

            using (StreamReader reader = new StreamReader(_jsonPath))
            {
                List<Flight> flights;
                string json = reader.ReadToEnd();
                if (json != null)
                {
                    flights = JsonConvert.DeserializeObject<List<Flight>>(json)!;
                    return flights;
                }else{
                    return new List<Flight>();
                }
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
