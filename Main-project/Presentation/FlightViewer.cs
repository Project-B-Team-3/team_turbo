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
    }
}
