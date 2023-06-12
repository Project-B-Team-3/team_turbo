using Main_project.DataModels;
using Newtonsoft.Json;

namespace Main_project.DataAccess;

public static class BookingDataAccess
{
	public static List<Booking> GetBookings()
	{
		var path = "./DataSources/Bookings.json";
		if (!File.Exists(path)) File.Create(path).Close();

		var json = File.ReadAllText(path);
		var bookings = JsonConvert.DeserializeObject<List<Booking>>(json);
		return bookings ?? new List<Booking>();
	}

	public static void CreateBooking(Booking booking)
	{
		var newBookings = GetBookings();
		newBookings.Add(booking);
		File.WriteAllText(
			"./DataSources/Bookings.json",
			JsonConvert.SerializeObject(newBookings, Formatting.Indented)
		);
		File.WriteAllLines($"./{booking.ReservationNumber}.txt", booking.GetLines());
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
}