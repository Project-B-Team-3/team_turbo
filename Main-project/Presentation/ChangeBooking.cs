namespace Main_project.Presentation;

public static class ChangeBooking
{
	public static void CancelBooking(int reservationNumber)
	{
		Console.WriteLine("Are you sure you want to cancel your booking? (y/n)");
		var key = Console.ReadKey().Key;
		if (key == ConsoleKey.Y)
		{
			var booking = DataAccess.BookingDataAccess.GetBookings()
				.First(u => u.ReservationNumber == reservationNumber);
			DataAccess.BookingDataAccess.RemoveBooking(booking);
			Console.WriteLine("Successfully removed booking!");
		}
	}
}