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

        public Flight(string number, string departure, string destination, DateTime departuretime, int seatCount, int premiumCount, decimal price)
        {
            FlightNumber = number;
            Departure = departure;
            Destination = destination;
            DepartureTime = departuretime;
            Seats = FlightDataAccess.GenerateSeats(seatCount, premiumCount);
            Price = price;
        }

        // Formatting class data
        public override string ToString()
        {
            return $"{Departure} to {Destination} at {DepartureTime.ToString("yyyy-MM-dd HH:mm:ss")} for {Price} ({Seats.Count(h => h.Available)} seats available)";
        }
    }
}