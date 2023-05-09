using System.Drawing;
using Main_project.Logic;

namespace Main_project.Presentation;

public static class SeatSelector
{
	public static string SelectSeat(string flightNumber)
	{
		Console.Clear();
		Console.WriteLine("The white seats are available:");
		foreach (var seat in BookingLogic.FlightSeats(flightNumber))
		{
			Console.ForegroundColor = !seat.Available ? ConsoleColor.Red : ConsoleColor.White;

			switch (seat.Number.ToCharArray()[0])
			{
				case 'D':
					Console.Write($"{seat.Number}\n", seat.Available ? Color.White : Color.Red);
					break;
				case 'B':
					Console.Write($"{seat.Number}  ", seat.Available ? Color.White : Color.Red);
					break;
				default:
					Console.Write($"{seat.Number} ", seat.Available ? Color.White : Color.Red);
					break;
			}
		}

		Console.Write("Which seat do you want? ");
		var chairNumber = Console.ReadLine();
		while (!BookingLogic.FlightSeats(flightNumber).Exists(h => h.Number == chairNumber) ||
		       !BookingLogic.FlightSeats(flightNumber).First(h => h.Number == chairNumber).Available ||
				chairNumber == null)
		{
			Console.WriteLine("Invalid choice, please choose again.");
			Console.Write("Which seat do you want? ");
			chairNumber = Console.ReadLine();
		}

		return chairNumber;
	}
}