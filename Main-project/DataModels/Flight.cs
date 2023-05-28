using Main_project.Logic;

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

        public Flight(string flightnumber, string departureairportcode, string destinationairportcode, DateTime departuretime, int rows, int economyCount, int businessCount, int firstCount, decimal price, string departureCity, string destinationCity)
        {
            FlightNumber = flightnumber;
            DepartureAirportCode = departureairportcode;
            DestinationAirportCode = destinationairportcode;
            DepartureTime = departuretime;
            Seats = SeatLogic.GenerateSeats(rows, economyCount, businessCount, firstCount);
            Price = price;
            DepartureCity = departureCity;
            DestinationCity = destinationCity;
        }

        // Formatting class data
        public override string ToString()
        {
            return $"{FlightNumber} - {DepartureCity} to {DestinationCity} - Departing {DepartureTime} - {Price:C}";
        }

    }
}
