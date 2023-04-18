using Main_project.DataModels;
using Newtonsoft.Json;

namespace Main_project.DataAccess;

public static class BookingDataAccess
{
	public static StreamReader BookingReader(){
		if (File.Exists("./DataSources/Bookings.json"))
		{
			return new StreamReader("./DataSources/Bookings.json");
		}
		var streamWriter = new StreamWriter("./DataSources/Bookings.json");
		streamWriter.Write("[]");
		streamWriter.Flush();
		streamWriter.Close();
		streamWriter.Dispose();
		return new StreamReader("./DataSources/Bookings.json");
	}

	public static StreamWriter BookingWriter()
	{
		if (File.Exists("./DataSources/Bookings.json"))
		{
			var streamWriter = new StreamWriter("./DataSources/Bookings.json");
			streamWriter.AutoFlush = true;
			return streamWriter;
		}
		else
		{
			File.Create("./DataSources/Bookings.json");
			var streamWriter = new StreamWriter("./DataSources/Bookings.json");
			streamWriter.AutoFlush = true;
			return streamWriter;
		}
	}

	public static List<Booking> GetBookings()
	{
		var json = BookingReader().ReadToEnd();
		var bookings = JsonConvert.DeserializeObject<List<Booking>>(json);
		return bookings ?? new List<Booking>();
	}

	public static void CreateBooking(Booking booking)
	{
		var total = GetBookings();
		total.Add(booking);
		BookingWriter().Write(total);
	}
}