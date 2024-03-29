using System.Globalization;
using Main_project.DataAccess;
using Main_project.Logic;

namespace Main_project.Presentation;

internal static class UserLogin
{
	public static void Login()
	{
		Console.WriteLine("Welcome to the login page");
		Console.WriteLine("Please enter your ReservationNumber:");
		var reservationNumber = Console.ReadLine();
		Console.WriteLine("Please enter your birthdate (dd-m-yyyy or dd m yyyy):");
		var Birthdate = Console.ReadLine();

		if (!DateTime.TryParseExact(Birthdate, new[] { "dd-M-yyyy", "dd M yyyy", "dd-MM-yyyy" },
			    CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthdate))
		{
			Console.WriteLine("Invalid date format. Please use dd-m-yyyy, dd m yyyy, or dd-MM-yyyy.");
			return;
		}

		var booking = BookingLogic.GetBookingByReservationNumber(reservationNumber, birthdate);
		if (booking == null)
		{
			Console.WriteLine("No booking found with that reservation number and birthdate");
			return;
		}

		var flight = FlightDataAccess.GetFlights().FirstOrDefault(h => h.FlightNumber == booking.FlightNumber);
		Console.WriteLine("Loading");
		if (flight != null)
			while (true)
			{
				Console.Clear();


				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Change booking\n");
				Console.ForegroundColor = ConsoleColor.White;

				Console.WriteLine("Your ticket:");

				Console.WriteLine("**************************************************");
				Console.WriteLine($"*{"Booking Details:",-50}*");
				Console.WriteLine($"*{"Reservation Number: " + booking.ReservationNumber,-50}*");
				Console.WriteLine($"*{"Flight Number: " + booking.FlightNumber,-50}*");
				Console.WriteLine($"*{"Departure Airport Code: " + flight.DepartureAirportCode,-50}*");
				Console.WriteLine($"*{"Destination Airport Code: " + flight.DestinationAirportCode,-50}*");
				Console.WriteLine($"*{"Departure Time: " + flight.DepartureTime.ToString("dd-M-yyyy HH:mm:ss"),-50}*");
				Console.WriteLine($"*{"Your reservation code is " + booking.ReservationNumber,-50}*");
				Console.WriteLine("**************************************************");


				Console.WriteLine("\nSelect an option:\n" +
				                  "[1] Delete booking\n" +
				                  "[2] Return to Admin");


				var input = Console.ReadKey(true);

				switch (input.Key)
				{
					case ConsoleKey.D1:
						Console.WriteLine("Delete booking");

						if (BookingDataAccess.GetBookings().Any(h => h.ReservationNumber == reservationNumber))
						{
							ChangeBooking.CancelBooking(reservationNumber);
							Console.WriteLine($"Your booking with number {reservationNumber} is cancelled!");
						}
						else
						{
							Console.WriteLine($"There is no booking with the number {reservationNumber}!");
						}

						Console.ReadKey(true);
						break;


					case ConsoleKey.D2:
						Console.WriteLine("Press enter to return to the menu...");
						return;

					default:
						Console.WriteLine("Invalid option.");
						break;
				}
			}

		Console.WriteLine("No booking found with that reservation number and birthdate");
	}
}