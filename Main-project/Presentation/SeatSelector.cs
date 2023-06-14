using System.Drawing;
using Main_project.DataModels;
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
					Console.Write($"{seat.Number}\n", color(seat));
					break;
				case 'B':
					Console.Write($"{seat.Number}  ", color(seat));
					break;
				default:
					Console.Write($"{seat.Number} ", color(seat));
					break;
			}
		}

		Console.Write("Which seat do you want? ");
		var chairNumber = Console.ReadLine()?.ToUpper();
		while (true)
		{
			if (BookingLogic.FlightSeats(flightNumber).Exists(h => h.Number == chairNumber) &&
			    BookingLogic.FlightSeats(flightNumber).First(h => h.Number == chairNumber).Available &&
			    chairNumber != null) return chairNumber;
			Console.WriteLine(!BookingLogic.FlightSeats(flightNumber).First(h => h.Number == chairNumber).Available
				? "This seat is already taken, please try again."
				: "Invalid seat number entered, please try again.");
			Console.Write("Which seat do you want? ");
			chairNumber = Console.ReadLine();
		}
	}

	public static ConsoleColor color(Seat seat)
	{
		if (!seat.Available)
		{
			return ConsoleColor.Red;
		}
		switch (seat.Class)
		{
			case "Business Class":
				return ConsoleColor.Blue;
			case "First Class":
				return ConsoleColor.DarkBlue;
			default:
				return ConsoleColor.White;
		}
	}
}