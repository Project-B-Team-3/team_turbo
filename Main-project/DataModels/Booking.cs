namespace Main_project.DataModels
{
    public class Booking
    {
        public string ReservationNumber { get; set; }
        public string FlightNumber { get; set; }
        public Dictionary<string, Person> Seats { get; set; }
        public Cost Cost { get; set; }

        public Booking(
            string reservationNumber,
            string flightNumber,
            Dictionary<string, Person> seats,
            Cost cost
        )
        {
            ReservationNumber = reservationNumber;
            FlightNumber = flightNumber;
            Seats = seats;
            Cost = cost;
        }
    }
}
