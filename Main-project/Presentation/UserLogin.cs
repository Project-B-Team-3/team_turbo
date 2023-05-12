using Main_project.Logic;
using System.Globalization;
using Main_project.DataModels;

namespace Main_project.Presentation
{
    static class UserLogin
    {
        static private BookingLogic bookingLogic = new BookingLogic();
        
        public static void Start()
        {
            Console.WriteLine("Welcome to the login page");
            Console.WriteLine("Please enter your ReservationNumber:");
            string reservationNumber = Console.ReadLine();
            Console.WriteLine("Please enter your birthdate (dd-m-yyyy or dd m yyyy):");
            string input = Console.ReadLine();

            if (!DateTime.TryParseExact(input, new[] { "dd-M-yyyy", "dd M yyyy", "dd-MM-yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out var Birthdate))
            {
                Console.WriteLine("Invalid date format. Please use dd-m-yyyy, dd m yyyy, or dd-MM-yyyy.");
                return;
            }

            Booking booking = bookingLogic.GetBookingByReservationNumber(reservationNumber);
            Console.WriteLine("Booking Details: " + booking?.ReservationNumber + " " + booking?.FlightNumber);



            if (booking != null && booking.Seats.TryGetValue(reservationNumber, out Person person) && person.BirthdateString == input)
            {
                Console.WriteLine("Welcome back " + person.Name);
                Console.WriteLine("Your reservation code is " + booking.ReservationNumber);

                //Write some code to go back to the menu
                Menu.Start();
            }
            else
            {
                Console.WriteLine("No booking found with that reservation number and birthdate");
            }

        }
    }
}