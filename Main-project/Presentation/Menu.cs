using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Misc;

namespace Main_project.Presentation;

public static class Menu
{
	public static void Start()
	{
		var x = false;
		var key = new ConsoleKeyInfo();
		while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape && x == false)
		{
			Console.Clear();
			Console.WriteLine(
				"What do you wish to do?\n[1] View all upcoming flights\n[2] Book a flight\n[3] Change a booking\n[4] Cancel a booking\n[5] Contact us\n[6] Quit\n"
			);

			key = Console.ReadKey(true);
			// Press key to trigger event (D0 = 0, D1 = 1, etc.)
			switch (key.Key)
			{
				case ConsoleKey.D1:
					ConsoleView.DisplayFlights();
					break;
				case ConsoleKey.D2:
					CreateBooking.CreateNewBooking();
					break;
				case ConsoleKey.D3:
					// TODO implement changing booking
					// UserLogin.Start();
					Console.WriteLine("You will be able to change your booking here...");
					Console.ReadKey(intercept: true);
					break;
				case ConsoleKey.D4:
					Console.WriteLine("Please enter your booking number:");
					var reservationNumber = Console.ReadLine();
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
				case ConsoleKey.D5:
					DisplayContactDetails();
					break;
				case ConsoleKey.D6:
				case ConsoleKey.Q:
					Console.WriteLine("Program has been quit");
					x = true;
					break;
				case ConsoleKey.G:
					FlightGenerator.GenerateFlights();
					break;
				case ConsoleKey.S:
					AdminPanel.Admin();
					break;
				default:
					// Checks for capslock/numlock
					if (Environment.OSVersion.Platform == PlatformID.Win32NT)
						if (Console.CapsLock && Console.NumberLock)
						{
							Console.WriteLine(key.KeyChar);
							Console.Write("Invalid option");
						}

					break;
			}
		}
	}

	private static void DisplayContactDetails() // Luuk you are free to move this :p I know hahahah.
	{
		var airportDataAccess = new AirportDataAccess();
		var airport = airportDataAccess.GetAirportDetails();

		Console.Clear();
		Console.WriteLine("Airport Contact Information");
		Console.WriteLine("Phone Number: " + airport.PhoneNumber);
		Console.WriteLine("Address: " + airport.Address);
		Console.WriteLine("Email: " + airport.Email);
		Console.WriteLine("\nPress any key to continue...");
		Console.ReadKey(true);
	}
}