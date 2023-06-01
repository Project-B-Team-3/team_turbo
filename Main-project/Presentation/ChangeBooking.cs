using Main_project.DataAccess;
using Main_project.Logic;

namespace Main_project.Presentation;

public static class ChangeBooking
{
	public static void CancelBooking(string reservationNumber)
	{
		var booking = BookingDataAccess
			.GetBookings()
			.FirstOrDefault(u => u.ReservationNumber == reservationNumber);
		if (booking == null)
		{
			Console.WriteLine("Invalid reservation number.");
			return;
		}

		Console.WriteLine("Are you sure you want to cancel your booking? (y/n)");
		var key = Console.ReadKey().Key;
		if (key == ConsoleKey.Y)
		{
			Console.WriteLine(booking.FlightNumber + " " + booking.ReservationNumber);

			BookingDataAccess.RemoveBooking(booking);
			foreach (var seatNum in booking.Seats) SeatLogic.UpdateSeat(booking.FlightNumber, seatNum.Key, false);
			Console.WriteLine("Successfully removed booking!");
		}
	}

	public static void ChangeSeat(string reservationNumber, DateTime birthday)
	{
		SeatLogic.ChangeSeat(reservationNumber, birthday);
	}
}