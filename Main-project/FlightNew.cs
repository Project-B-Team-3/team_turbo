namespace Main_project
{
    public class Flight
    {
        public string FlightNumber { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public int SeatsAvailable { get; set; }
        public decimal Price { get; set; }

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
            return $"{Departure} to {Destination} at {DepartureTime.ToString("yyyy-MM-dd HH:mm:ss")} for {Price:C} ({SeatsAvailable} seats available)";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Temporary flights dataset to test code with
            List<Flight> flights = new List<Flight>();
            flights.Add(new Flight("LAX01", "JFK", "LAX", new DateTime(2023, 04, 01, 08, 30, 00), 100, 250.00m));
            flights.Add(new Flight("JFK01", "LAX", "JFK", new DateTime(2023, 04, 01, 12, 45, 00), 50, 300.00m));
            flights.Add(new Flight("LAX02", "ORD", "LAX", new DateTime(2023, 04, 02, 10, 15, 00), 75, 200.00m));
            flights.Add(new Flight("ORD01", "LAX", "ORD", new DateTime(2023, 04, 02, 14, 30, 00), 25, 350.00m));

            foreach (Flight flight in flights)
            {
                Console.WriteLine(flight.ToString());
            }

            // Prompt the user to select a flight
            int selection = 0;
            do
            {
                Console.Write("Enter the number of the flight you want to book: ");
            } while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > flights.Count);

            // Book the selected flight
            Flight selectedFlight = flights[selection - 1];
            selectedFlight.SeatsAvailable--;
            Console.WriteLine($"You have booked the following flight:\n{selectedFlight.ToString()}");

            Console.ReadLine();
        }
    }
}