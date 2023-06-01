using System;
using System.Globalization;
using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Logic;

namespace Main_project.Presentation;

internal static class UserLogin
{
	private static BookingLogic bookingLogic = new();

	public static void Start()
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

		var booking = bookingLogic.GetBookingByReservationNumber(reservationNumber, birthdate);
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
		}
		else
		{
			Console.WriteLine("No booking found with that reservation number and birthdate");
		}
	}
}