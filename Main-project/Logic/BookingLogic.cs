using Main_project.DataAccess;
using Main_project.DataModels;
using Newtonsoft.Json;
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
        
        public Booking GetBookingByReservationNumber(string reservationNumber, string Birthdate)
        {
            var bookings = BookingDataAccess.GetBookings();
            var booking = bookings.FirstOrDefault(b => b.ReservationNumber == reservationNumber &&
                                                       b.Seats.Any(s => s.Value.Birthdate == Birthdate));
            if (booking != null)
            {
                var flight = FlightDataAccess.GetFlights().FirstOrDefault(f => f.FlightNumber == booking.FlightNumber);
                booking.Flight = flight;
            }

            return booking;
        }
    }
}

