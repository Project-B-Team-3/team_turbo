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
		Console.ForegroundColor = ConsoleColor.DarkBlue;
		Console.WriteLine("The dark blue seats are First Class");
		Console.ForegroundColor = ConsoleColor.Blue;
		Console.WriteLine("The blue seats are Business Class");
		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine("The white seats are Economy Class");
		foreach (var seat in BookingLogic.FlightSeats(flightNumber))
		{
			Console.ForegroundColor = color(seat);

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
		if (!seat.Available) return ConsoleColor.Red;
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