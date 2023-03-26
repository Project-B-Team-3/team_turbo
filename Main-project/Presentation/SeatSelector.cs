using Main_project.Logic;

namespace Main_project.Presentation;

public class SeatSelector
{
	public static void SelectSeat()
	{
		Console.Clear();
		Console.WriteLine("You can select your seat under here:");
		foreach (var seat in BookingLogic.FlightSeats())
		{
			Console.WriteLine(seat.Number);
		}
	}
}