using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Logic;

namespace Main_project.Presentation;

public static class CreateBooking
{
    public static void CreateNewBooking()
    {
        decimal catering;
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

        Console.WriteLine("Would you like to add catering to your booking? (y/n)");
        var answer = Console.ReadLine()?.ToLower();
        if (answer == "y")
        {
            var cateringList = CateringLogic.cateringList();
            Console.WriteLine("Please choose from the following menu:");
            for (int i = 0; i < cateringList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cateringList[i].Name} ({cateringList[i].Price:C2}) - {cateringList[i].Description}");
            }
            var choices = new List<int>();
            bool invalidInput;
            do
            {
                invalidInput = false;
                Console.WriteLine("Enter the number of the catering item you want to order (or '0' to exit):");
                var choiceInput = Console.ReadLine();
                int choice;
                if (!int.TryParse(choiceInput, out choice))
                {
                    invalidInput = true;
                    Console.WriteLine("Invalid input, please try again...");
                    Thread.Sleep(200);
                }
                else if (choice < 0 || choice > cateringList.Count)
                {
                    invalidInput = true;
                    Console.WriteLine("Invalid input, please try again...");
                    Thread.Sleep(200);
                }
                else if (choice == 0)
                {
                    break;
                }
                else
                {
                    choices.Add(choice - 1);
                }
            } while (invalidInput || choices.Count < cateringList.Count);

            var cateringItems = new List<Catering>();
            var totalPrice = 0M;
            foreach (var choice in choices)
            {
                var cateringItem = cateringList[choice];
                cateringItems.Add(cateringItem);
                totalPrice += cateringItem.Price;
            }

            Console.WriteLine("Catering items ordered: ");
            foreach (var cateringItem in cateringItems)
            {
                Console.WriteLine($"{cateringItem.Name}, ");
            }

            catering = totalPrice;
            Console.WriteLine($"Total price: {totalPrice:C2}");
            Thread.Sleep(200);
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

        // TODO actually integrate pricing
        Cost cost = new(5, 5, new List<int>());
        BookingDataAccess.CreateBooking(new Booking(reservationNum, flightNum, seats, cost));
    }
}