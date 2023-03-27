using Main_project.DataAccess;
using Main_project.DataModels;

namespace Main_project.Logic
{
    public class BookingLogic
    {
        public static IEnumerable<Flight> UpComingFlights(){
            var displayedFlights = FlightDataAccess.GetFlights().Where(f => f.DepartureTime > DateTime.Now)
                .Where(f => f.DepartureTime < DateTime.Now + TimeSpan.FromDays(28));
            return displayedFlights;
        }
        public IEnumerable<Flight> GetAllFlights()
        {
            return flightData.GetFlights();
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return flightData.GetFlights();
        }

        public static List<Seat> FlightSeats(string flightNumber)
        {
            return FlightDataAccess.GetFlights().Where(h => h.FlightNumber == flightNumber).Select(h => h.Seats).First();
        }
    }
}
