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
		var flight = FlightDataAccess.GetFlights().First(h => h.FlightNumber == booking.FlightNumber);
		if (booking != null)
		{
			Console.WriteLine("Booking Details:");
			Console.WriteLine("Reservation Number: " + booking.ReservationNumber);
			Console.WriteLine("Flight Number: " + booking.FlightNumber);
			Console.WriteLine("Departure Airport Code: " + flight.DepartureAirportCode);
			Console.WriteLine("Destination Airport Code: " + flight.DestinationAirportCode);
			Console.WriteLine("Departure Time: " + flight.DepartureTime.ToString("dd-M-yyyy HH:mm:ss"));

			Console.WriteLine("Your reservation code is " + booking.ReservationNumber);

			while (true)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("change booking\n");
				Console.ForegroundColor = ConsoleColor.White;

				Console.WriteLine("The list of available caterings is:");

				Console.WriteLine("\nSelect an option:\n" +
				                  "[1] Delete booking\n" +
				                  "[2] Change booking\n" +
				                  "[3] Return to Admin");


				ConsoleKeyInfo input = Console.ReadKey(true);

				switch (input.Key)
				{
					case ConsoleKey.D1:
						Console.WriteLine("Delete booking");

						if(BookingDataAccess.GetBookings().Any(h => h.ReservationNumber == reservationNumber)){
							ChangeBooking.CancelBooking(reservationNumber);
							Console.WriteLine($"Your booking with number {reservationNumber} is cancelled!");
						}
						else
						{
							Console.WriteLine($"There is no booking with the number {reservationNumber}!");
						}

						Console.ReadKey(intercept: true);
						break;

					case ConsoleKey.D2:
						Console.WriteLine("Change booking");
						
						break;

					case ConsoleKey.D3:
						Console.WriteLine("return to menu.");
						Menu.Start();
						break;

					default:
						Console.WriteLine("Invalid option.");
						break;
				}
			}
		}
		
		else
		{
			Console.WriteLine("No booking found with that reservation number and birthdate");
		}
	}
}