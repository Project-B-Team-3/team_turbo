using Main_project.DataAccess;
using Main_project.DataModels;
using System.Text.RegularExpressions;

namespace Main_project.Logic
{
    public static class BookingLogic
    {
        public static IEnumerable<Flight> UpComingFlights()
        {
            var displayedFlights = FlightDataAccess
                .GetFlights()
                .Where(f => f.DepartureTime > DateTime.Now)
                .Where(f => f.DepartureTime < DateTime.Now + TimeSpan.FromDays(28));
            return displayedFlights;
        }

        public static List<Flight> GetAllFlights()
        {
            return FlightDataAccess.GetFlights();
        }

        public static List<Seat> FlightSeats(string flightNumber)
        {
            return FlightDataAccess
                .GetFlights()
                .Where(h => h.FlightNumber == flightNumber)
                .Select(h => h.Seats)
                .First();
        }

        public static Flight GetFlightByNumber(string flightNumber)
        {
            return FlightDataAccess
                .GetFlights()
                .FirstOrDefault(f => f.FlightNumber == flightNumber);
        }

        public static Booking GetBookingByReservationNumber(string reservationNumber, DateTime birthdate)
        {
            var bookings = BookingDataAccess.GetBookings();
            var booking = bookings.FirstOrDefault(
                b =>
                    b.ReservationNumber == reservationNumber
                    && b.Seats.Any(s => s.Value.Birthdate == birthdate)
            );
            return booking;
        }

        public static string GenerateUniqueReservationCode()
        {
            string reservationCode;
            Random random = new Random();
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@!])[A-Za-z\d@!]{8,}$");

            do
            {
                reservationCode = GenerateRandomCode();
            } while (!regex.IsMatch(reservationCode));

            return reservationCode;
        }

        public static string GenerateRandomCode()
        {
            const string characters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@!";
            Random random = new Random();
            string code = "";

            for (int i = 0; i < 8; i++)
            {
                int index = random.Next(0, characters.Length);
                code += characters[index];
            }

            return code;
        }
    }
}
