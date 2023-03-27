using Main_project.DataAccess;

namespace Main_project.DataModels
{
    public class Flight
    {
        public string FlightNumber { get; set; }
        public string DepartureAirportCode { get; set; } 
        public string DestinationAirportCode { get; set; }
        public DateTime DepartureTime { get; set; }
        public List<Seat> Seats { get; set; }
        public decimal Price { get; set; }
        public string DepartureCity { get; set; }
        public string DestinationCity { get; set; }

        public Flight(string flightnumber, string departureairportcode, string destinationairportcode, DateTime departuretime, int seats, decimal price, string departurecity, string destinationcity)
        {
            FlightNumber = flightnumber;
            DepartureAirportCode = departureairportcode;
            DestinationAirportCode = destinationairportcode;
            DepartureTime = departuretime;
            Seats = FlightDataAccess.GenerateSeats(seatCount, premiumCount);
            Price = price;
            DepartureCity = departureCity;
            DestinationCity = destinationCity;
        }

        // Formatting class data
        public override string ToString()
        {
            return $"{Departure} to {Destination} at {DepartureTime.ToString("yyyy-MM-dd HH:mm:ss")} for {Price} ({Seats.Count(h => h.Available)} seats available)";
        }
    }
}
