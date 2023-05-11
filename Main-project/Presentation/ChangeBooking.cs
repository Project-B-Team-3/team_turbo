using Main_project.DataAccess;

namespace Main_project.Presentation;

public static class ChangeBooking
{
	public static void CancelBooking(string reservationNumber)
	{
		Console.WriteLine("Are you sure you want to cancel your booking? (y/n)");
		var key = Console.ReadKey().Key;
		if (key == ConsoleKey.Y)
		{
			var booking = BookingDataAccess.GetBookings()
				.First(u => u.ReservationNumber == reservationNumber);
			Console.WriteLine(booking.FlightNumber + " " + booking.ReservationNumber);

			BookingDataAccess.RemoveBooking(booking);
			foreach (var seatNum in booking.Seats)
			{
				FlightDataAccess.UpdateSeat(booking.FlightNumber, seatNum.Key, false);
			}
			Console.WriteLine("Successfully removed booking!");
		}
	}
}