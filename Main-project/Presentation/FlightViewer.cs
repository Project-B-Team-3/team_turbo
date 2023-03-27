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

        // public int GetFlightSelection()
        // {
        //     int selection = 0;
        //     do
        //     {
        //         Console.Write("Enter the number of the flight you want to book: ");
        //     } while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > flights.Count);
        //     return selection;
        // }
    }
}
