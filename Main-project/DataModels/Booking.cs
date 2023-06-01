namespace Main_project.DataModels;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Booking
    {
        private string reservationNumber;
        public string ReservationNumber
        {
            get { return reservationNumber; }
            set
            {
                if (!IsValidReservationCode(value))
                {
                    throw new ArgumentException("Invalid reservation code.");
                }
                reservationNumber = value;
            }
        }

        public string FlightNumber { get; set; }
        public Dictionary<string, Person> Seats { get; set; }
        public Cost Cost { get; set; }

        public Booking(string reservationNumber, string flightNumber, Dictionary<string, Person> seats, Cost cost)
        {
            ReservationNumber = reservationNumber;
            FlightNumber = flightNumber;
            Seats = seats;
            Cost = cost;
        }

        private bool IsValidReservationCode(string code)
        {
            if (code.Length < 8)
                return false;

            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(code);
        }
    }
}