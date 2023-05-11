using Newtonsoft.Json;
using Main_project.DataModels;

namespace Main_project.DataAccess
{
    public static class FlightDataAccess
    {
        public static void InitFiles()
        {
            if (File.Exists("./DataSources/Flights.json")) return;
            File.WriteAllText("./DataSources/Flights.json", "[]");
        }

        public static List<Flight> GetFlights()
        {
            try
            {
                var json = File.ReadAllText("./DataSources/Flights.json");
                var flights = JsonConvert.DeserializeObject<List<Flight>>(json);
                return flights ?? new List<Flight>();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e);
                Environment.Exit(1);
                return new List<Flight>();
            }
        }

        public static List<Seat> GenerateSeats(int seatCount, int businessCount)
        {
            var rows = "ABCDEFGHJKL".ToCharArray();
            var returnSeats = new List<Seat>();
            for (var i = 0; i < seatCount; i++)
            {
                returnSeats.Add(new Seat
                    {
                        Number = rows[i % 4] + (i / 4 + 1).ToString(),
                        Available = true,
                        Class = i < businessCount ? "Business Class" : "Economy Class"
                    }
                );
            }

            return returnSeats;
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

            int index = flights.FindIndex(f => f.FlightNumber == flight.FlightNumber);

            if (index != -1)
            {
                flights[index] = flight;
                File.WriteAllText("./DataSources/Flights.json", JsonConvert.SerializeObject(flights, Formatting.Indented));
            }
        }
        
        public static void UpdateSeat(string flightNum, string seat, bool available){
            var flight = GetFlights().First(h => h.FlightNumber == flightNum);
            flight.Seats.First(h => h.Number == seat).Available = available;
            UpdateFlight(flight);
        }
    }
}