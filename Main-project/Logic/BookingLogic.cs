using Main_project.DataAccess;
using Main_project.DataModels;

using System.Linq;

namespace Main_project.Logic
{
    public class BookingLogic
    {
        public static IEnumerable<Flight> UpComingFlights(){
            var displayedFlights = FlightDataAccess.GetFlights().Where(f => f.DepartureTime > DateTime.Now)
                .Where(f => f.DepartureTime < DateTime.Now + TimeSpan.FromDays(28));
            return displayedFlights;
        }
        public static List<Flight> GetAllFlights()
        {
            return FlightDataAccess.GetFlights();
        }

        public static List<Seat> FlightSeats(string flightNumber)
        {
            return FlightDataAccess.GetFlights().Where(h => h.FlightNumber == flightNumber).Select(h => h.Seats).First();
        }
        public static Flight GetFlightByNumber(string flightNumber)
        {
            return FlightDataAccess.GetFlights().FirstOrDefault(f => f.FlightNumber == flightNumber);
        }
        public static Booking GetBookingByFlightNumberAndBirthdate(string flightNumber, DateTime birthdate)
        {
            var bookings = BookingDataAccess.GetBookingsByFlightNumberAndBirthdate(flightNumber, birthdate);
            return bookings.FirstOrDefault();
        }

        public static bool CheckBooking(string flightNumber, DateTime birthdate)
        {
            var booking = BookingDataAccess.GetBookingsByFlightNumberAndBirthdate(flightNumber, birthdate);
            return booking != null;
        }


    }
}