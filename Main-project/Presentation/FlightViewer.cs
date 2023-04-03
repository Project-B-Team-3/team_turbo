using Main_project.DataAccess;
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

		public static void DisplayAllFlights()
        {
            foreach (var flight in BookingLogic.GetAllFlights())
            {
                Console.WriteLine($"Flight number: {flight.FlightNumber}");
                Console.WriteLine($"Departure: {flight.DepartureCity}");
                Console.WriteLine($"Destination: {flight.DestinationCity}");
                Console.WriteLine($"Departure time: {flight.DepartureTime}");
                Console.WriteLine($"Seats available: {flight.Seats.Count}");
                Console.WriteLine($"Price: {flight.Price}");
                Console.WriteLine();
            }
        }

        public static void SelectFlight()
        {
            // Get list of upcoming flights
            var flights = BookingLogic.UpComingFlights();

            // If no upcoming flights, display message and return
            if (!flights.Any())
            {
                Console.WriteLine("No upcoming flights!");
                Console.ReadKey();
                return;
            }

            // Display list of flights for user to select from
            Console.WriteLine("Select a flight:");

            for (var i = 0; i < flights.Count(); i++)
            {
                Console.WriteLine($"{i + 1}: {flights.ElementAt(i).FlightNumber}");
            }

            // Get user input for flight selection
            Console.Write("Enter the number of the flight you want to select: ");
            var input = Console.ReadLine();

            // If input is valid, get selected flight from FlightDataAccess and display selected flight
            if (int.TryParse(input, out var index) && index >= 1 && index <= flights.Count())
            {
                var flight = FlightDataAccess.GetFlights().FirstOrDefault(f => f.FlightNumber == flights.ElementAt(index - 1).FlightNumber);
                if (flight != null)
                {
                    Console.WriteLine($"Selected flight: {flight.FlightNumber}");
                    // @@@TO DO@@@ Add code here later to actually book the flight? Or do something with the selected flight?
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid flight number.");
                }
            }
            else // If input is not valid, display error message
            {
                Console.WriteLine("Invalid input. Please enter a valid flight number.");
            }

            Console.ReadKey();
        }
    }
}
