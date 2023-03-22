using Main_project.Logic;

namespace Main_project.Presentation
{
    public class ConsoleView
    {
        private BookingLogic _bookingLogic;

        public ConsoleView()
        {
            _bookingLogic = new BookingLogic();
        }

        public void DisplayFlights()
        {
            List<Flight> flights = _bookingLogic.GetAllFlights();
            foreach (Flight flight in flights)
            {
                Console.WriteLine(flight.ToString());
            }
        }

        public int GetFlightSelection()
        {
            int selection = 0;
            do
            {
                Console.Write("Enter the number of the flight you want to book: ");
            } while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > flights.Count);
            return selection;
        }

        public void BookFlight(int selection)
        {
            _bookingLogic.BookFlight(selection);
        }

        // other methods for handling user input/output
    }
}
