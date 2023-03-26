namespace Main_project.DataModels
{
    public class FlightGenerator
    {
        int flightNumberCounter = 1;
        List<Flight> flights = new List<Flight>();
        Random rand = new Random();

        // Airport locations stored in a dictionary
        Dictionary<string, string> airportCodes = new Dictionary<string, string>() {
            // Europe
            {"LHR", "London"}, {"CDG", "Paris"}, {"FRA", "Frankfurt"}, {"BCN", "Barcelona"}, {"MAD", "Madrid"},
            {"AMS", "Amsterdam"}, {"IST", "Istanbul"}, {"ATH", "Athens"}, {"CPH", "Copenhagen"}, {"ZRH", "Zurich"},
            // America
            {"JFK", "New York"}, {"LAX", "Los Angeles"}, {"ORD", "Chicago"}, {"SFO", "San Francisco"}, {"MCO", "Orlando"},
            {"DEN", "Denver"}, {"SEA", "Seattle"}, {"MIA", "Miami"}, {"LAS", "Las Vegas"}, {"ATL", "Atlanta"},
            // Asia
            {"HND", "Tokyo"}, {"NRT", "Tokyo"}, {"PEK", "Beijing"}, {"HKG", "Hong Kong"}, {"DXB", "Dubai"},
            {"BKK", "Bangkok"}, {"ICN", "Seoul"}, {"KUL", "Kuala Lumpur"}, {"SGN", "Ho Chi Minh City"}, {"DEL", "New Delhi"}
            };

        // Generating the next month of flights
        DateTime startDate = new DateTime(2023, 4, 1);
        DateTime endDate = new DateTime(2023, 4, 30);

        public FlightGenerator()
        {
            // Loop through all pairs departpure and destination airports
            foreach (var departureAirport in airportCodes)
            {
                foreach (var destinationAirport in airportCodes)
                {
                    // Check if departure and destination airports are different
                    if (departureAirport.Key != destinationAirport.Key)
                    {
                        // Loop through all days in specified time
                        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                        {
                            var flight = new Flight
                            (
                                $"ROT{flightNumberCounter:D3}", //:D3 formats int as string with at least 3 digits
                                departureAirport.Key,
                                destinationAirport.Key,
                                new DateTime(date.Year, date.Month, date.Day, rand.Next(0, 24), 0, 0), // Likely won't be random at end product, was ez to implement for now
                                rand.Next(50, 200), // Was ez to implement for wholistic sake, waiting for Luuk to be done with that part of the code
                                rand.Next(100, 1000), // Was ez to implement, waiting for this user story to be picked up in either sprint 2 or 3 to finalize this piece of code
                                departureAirport.Value,
                                destinationAirport.Value
                            );
                            flights.Add(flight);
                            flightNumberCounter++;
                        }
                    }
                }
            }
        }
    }
}
