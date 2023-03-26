namespace Main_project.DataModels
{
    public class Flight
    {
        public string FlightNumber { get; set; }
        public string DepartureAirportCode { get; set; } 
        public string DestinationAirportCode { get; set; }
        public DateTime DepartureTime { get; set; }
        public int SeatsAvailable { get; set; }
        public decimal Price { get; set; }
        public string DepartureCity { get; set; }
        public string DestinationCity { get; set; }

        public Flight(string number, string departure, string destination, DateTime departuretime, int seats, decimal price)
        {
            FlightNumber = number;
            Departure = departure;
            Destination = destination;
            DepartureTime = departuretime;
            SeatsAvailable = seats;
            Price = price;
        }

        // Formatting class data
        public override string ToString()
        {
            return $"{DepartureCity} to {DestinationCity} at {DepartureTime.ToString("yyyy-MM-dd HH:mm:ss")} for {Price} ({SeatsAvailable} seats available)";
        }
    }
}