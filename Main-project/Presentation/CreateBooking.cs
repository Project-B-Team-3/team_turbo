using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Logic;

namespace Main_project.Presentation;

public static class CreateBooking
{
    public static void CreateNewBooking()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the booking menu, please enter a flight number to book a flight.");
        var flightNum = Console.ReadLine()?.ToUpper();
        if (BookingLogic.UpComingFlights().All(h => h.FlightNumber != flightNum))
        {
            Console.WriteLine("This flight does not exist...");
            Thread.Sleep(200);
            return;
        }
        Console.WriteLine("For how many people would you like to book?");
        var peopleCount = Console.ReadLine();
        var peopleInt = 0;
        if (flightNum is null || peopleCount is null ||
            flightNum == "" || peopleCount == "" || !int.TryParse(peopleCount, out peopleInt))
        {
            Console.WriteLine("Wrong input...");
            Console.ReadKey();
            return;
        }

        Dictionary<string, Person> seats = new();
        for (int i = 0; i < peopleInt; i++)
        {
            Console.WriteLine($"What is the name of person #{i+1}?");
            var name = Console.ReadLine();
            Console.WriteLine("What is the birthdate of this person? (Format: dd-MM-yyyy)");
            var birthdateInput = Console.ReadLine();
            if (!DateTime.TryParseExact(birthdateInput, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var birthdate))
            {
                Console.WriteLine("Invalid date entered, please try again...");
                Thread.Sleep(200);
                Console.Clear();
                i--;
                continue;
            }

            Console.WriteLine("What is the ID, driver's license or passport document number of this person?");
            var docNum = Console.ReadLine();
            if (name is null || birthdateInput is null || docNum is null ||
                name == "" || birthdateInput == "" || docNum == "")
            {
                Console.WriteLine("Invalid data entered, please try again...");
                Thread.Sleep(200);
                Console.Clear();
                i--;
                continue;
            }

            var seat = SeatSelector.SelectSeat(flightNum);
            Console.WriteLine("Writing data...");
            SeatLogic.UpdateSeat(flightNum, seat, false);
            seats.Add(seat, new Person(name, birthdateInput, docNum));
            Console.WriteLine("Successfully added another person to the booking.");
            Thread.Sleep(200);
        }

        var reservationNum = Random.Shared.Next().ToString();

        BookingDataAccess.CreateBooking(new Booking(reservationNum, flightNum, seats));
    }
}