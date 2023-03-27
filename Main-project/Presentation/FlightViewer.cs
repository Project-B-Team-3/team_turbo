using Main_project.Logic;

namespace Main_project.Presentation
{
    public static class ConsoleView
    {
        public static void DisplayFlights()
        {
            if (!BookingLogic.UpComingFlights().Any())
            {
                Console.WriteLine("No upcoming flights!");
                Console.ReadKey();
            }
            else
            {
                foreach (var flight in BookingLogic.UpComingFlights())
                {
                    Console.WriteLine(flight);
                }

                Console.ReadKey();
            }
        }

		public void DisplayAllFlights()
        {
            foreach (var flight in _bookingLogic.GetAllFlights())
            {
                Console.WriteLine($"Flight number: {flight.FlightNumber}");
                Console.WriteLine($"Departure: {flight.Departure}");
                Console.WriteLine($"Destination: {flight.Destination}");
                Console.WriteLine($"Departure time: {flight.DepartureTime}");
                Console.WriteLine($"Seats available: {flight.SeatsAvailable}");
                Console.WriteLine($"Price: {flight.Price}");
                Console.WriteLine();
            }
        }
    }
}
