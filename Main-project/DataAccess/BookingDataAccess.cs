using Main_project.DataModels;
using Newtonsoft.Json;

namespace Main_project.DataAccess;

public static class BookingDataAccess
{
	public static List<Booking> GetBookings()
	{
		try
		{
			var json = File.ReadAllText("./DataSources/Bookings.json");
			var bookings = JsonConvert.DeserializeObject<List<Booking>>(json);
			return bookings ?? new List<Booking>();
		}
		catch (Exception e)
		{
			Console.Clear();
			Console.WriteLine(e);
			Environment.Exit(1);
			return new List<Booking>();
		}
	}

	public static void CreateBooking(Booking booking)
	{
		if (!IsValidReservationCode(booking.ReservationNumber))
		{
			Console.WriteLine("Invalid reservation code.");
			return;
		}

		var newBookings = GetBookings();
		newBookings.Add(booking);
		File.WriteAllText(
			"./DataSources/Bookings.json",
			JsonConvert.SerializeObject(newBookings, Formatting.Indented)
		);
	}

	public static void RemoveBooking(Booking booking)
	{
		var newBookings = GetBookings();
		if (newBookings.RemoveAll(h => h.ReservationNumber == booking.ReservationNumber) == 1)
			Console.WriteLine("Successfully removed booking!");
		else
			Console.WriteLine("Could not find booking!");
		File.WriteAllText(
			"./DataSources/Bookings.json",
			JsonConvert.SerializeObject(newBookings, Formatting.Indented)
		);
	}

	private static bool IsValidReservationCode(string reservationCode)
	{
		if (reservationCode.Length < 8)
			return false;

		var containsLetters = false;
		var containsNumbers = false;
		var containsCapitalizedLetters = false;
		var containsUncapitalizedLetters = false;

		foreach (var c in reservationCode)
			if (char.IsLetter(c))
			{
				containsLetters = true;
				if (char.IsUpper(c))
					containsCapitalizedLetters = true;
				else
					containsUncapitalizedLetters = true;
			}
			else if (char.IsDigit(c))
			{
				containsNumbers = true;
			}

		return containsLetters && containsNumbers && containsCapitalizedLetters && containsUncapitalizedLetters;
	}
}