using Main_project.Logic;
using Main_project.DataModels;

namespace Main_project.Presentation
{
    public class ConsoleView
    {
        private readonly BookingLogic _bookingLogic;

        public ConsoleView()
        {
            _bookingLogic = new BookingLogic();
        }

        public void DisplayFlights()
        {
            if (!_bookingLogic.UpComingFlights().Any())
            {
                Console.WriteLine("No upcoming flights!");
                Console.ReadKey();
            }
            else
            {
                foreach (var flight in _bookingLogic.UpComingFlights())
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
