using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Presentation;

namespace Main_project.Logic;

public static class SeatLogic
{
	public static void ChangeSeat(string reservationNumber, string birthday){
		var flight = BookingDataAccess.GetBookings().First(u => u.ReservationNumber == reservationNumber);
		var person = flight.Seats
			.First(h => h.Value.Birthdate == birthday);
		var seat = SeatSelector.SelectSeat(flight.FlightNumber);
		UpdateSeat(flight.FlightNumber, person.Key, true);
		UpdateSeat(flight.FlightNumber, seat, false);
		flight.Seats.Remove(person.Key);
		flight.Seats.Add(seat, person.Value);
	}

	public static void UpdateSeat(string flightNum, string seat, bool available){
		var flight = FlightDataAccess.GetFlights().First(h => h.FlightNumber == flightNum);
		flight.Seats.First(h => h.Number == seat).Available = available;
		FlightDataAccess.UpdateFlight(flight);
	}

	public static List<Seat> GenerateSeats(int rowsCount, int economyCount, int businessCount, int firstCount)
	{
		var rows = "ABCDEFGHJKL".ToCharArray();
		var returnSeats = new List<Seat>();
		for (var i = 0; i < economyCount + businessCount + firstCount; i++)
		{
			returnSeats.Add(new Seat
				{
					Number = rows[i % rowsCount] + (i / rowsCount + 1).ToString(),
					Available = true,
					Class = i < firstCount ? "First Class" :
						i < firstCount + businessCount ? "Business Class" : "Economy Class"
				}
			);
		}

		return returnSeats;
	}
}