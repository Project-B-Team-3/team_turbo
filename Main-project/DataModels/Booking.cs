using Main_project.DataAccess;

namespace Main_project.DataModels
{
    public class Booking
    {
        public string ReservationNumber { get; set; }
        public string FlightNumber { get; set; }
        public Dictionary<string, Person> Seats { get; set; }
        public Cost Cost { get; set; }

        public Booking(
            string reservationNumber,
            string flightNumber,
            Dictionary<string, Person> seats,
            Cost cost
        )
        {
            ReservationNumber = reservationNumber;
            FlightNumber = flightNumber;
            Seats = seats ?? new Dictionary<string, Person>(); // Initialize Seats if it's null
            Cost = cost;
        }

    public List<string> GetLines()
    {
        var flight = FlightDataAccess
            .GetFlights()
            .FirstOrDefault(h => h.FlightNumber == FlightNumber);

        if (flight == null)
        {
            // Handle the scenario when no flight is found with the specified flight number
            return new List<string> { "Flight details not found." };
        }

        string[] lines =
        {
            "Booking Details",
            $"Departure City: {flight.DepartureCity}",
            $"Destination City: {flight.DestinationCity}",
            $"Reservation Number: {ReservationNumber}",
            $"Flight Number: {FlightNumber}",
            $"Departure Airport Code: {flight.DepartureAirportCode}",
            $"Destination Airport Code: {flight.DestinationAirportCode}",
            $"Departure Time: {flight.DepartureTime:dd-M-yyy HH:mm:ss}",
            $"For the total price of: {Cost.GetTotal()}",
            "",
            "And your seats are:"
        };

        var returnVal = lines.ToList();
        foreach (var person in Seats) returnVal.Add(person.Value + ", " + person.Key);
        return returnVal;
    }
}
}