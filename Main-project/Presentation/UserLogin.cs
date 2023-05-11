using Main_project.Logic;
using System.Globalization;
using Main_project.DataModels;

namespace Main_project.Presentation
{
    static class UserLogin
    {
        static private AccountsLogic accountsLogic = new AccountsLogic();
        static private BookingLogic bookingLogic = new BookingLogic();

        public static void Start()
        {
            Console.WriteLine("Welcome to the login page");
            Console.WriteLine("Please enter your FlightNumber (reservation code)");
            string flightNumber = Console.ReadLine();
            Console.WriteLine("Please enter your birthdate (dd-m-yyyy or dd m yyyy)");
            string input = Console.ReadLine();
            DateTime birthdate;
            if (!DateTime.TryParseExact(input, new[] { "dd-M-yyyy", "dd M yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out birthdate))
            {
                Console.WriteLine("Invalid date format. Please use dd-m-yyyy or dd m yyyy.");
                return;
            }

            bool bookingExists = BookingLogic.CheckBooking(flightNumber, birthdate);
            if (bookingExists)
            {
                Booking booking = BookingLogic.GetBookingByFlightNumberAndBirthdate(flightNumber, birthdate);
                Console.WriteLine("Welcome back " + booking.Seats["A1"].Name);
                Console.WriteLine("Your reservation code is " + booking.ReservationNumber);

                //Write some code to go back to the menu
                Menu.Start();
            }
            else
            {
                Console.WriteLine("No booking found with that flight number and birthdate");
            }
        }
    }
}