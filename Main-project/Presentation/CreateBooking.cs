using System.Globalization;
using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Logic;

namespace Main_project.Presentation
{
    public class CreateBooking
    {
        public static void CreateNewBooking()
        {
            var cost = new Cost(0, 0, new List<decimal>());
            Console.Clear();
            Console.WriteLine(
                "Welcome to the booking menu. Please wait while the list of destinations is being loaded..."
            );

            var destinations = FlightDataAccess.GetDestinations();

            Console.Clear();
            Console.WriteLine("List of destinations loaded. Please select a destination:");

            for (var i = 0; i < destinations.Count; i++)
            {
                var destination = destinations[i];
                Console.WriteLine($"{i + 1}. {destination}");
            }

            Console.WriteLine();
            Console.WriteLine("Enter the number corresponding to your desired destination:");
            var destinationInput = Console.ReadLine();
            if (
                !int.TryParse(destinationInput, out var destinationIndex)
                || destinationIndex < 1
                || destinationIndex > destinations.Count
            )
            {
                Console.WriteLine("Invalid input, please try again...");
                Thread.Sleep(200);
                return;
            }

            var selectedDestination = destinations[destinationIndex - 1];

            var flights = FlightDataAccess.GetFlightsByDestination(selectedDestination);
            if (flights.Count == 0)
            {
                Console.WriteLine($"There are no available flights to {selectedDestination}...");
                Thread.Sleep(200);
                return;
            }

            Console.WriteLine("Please enter your desired travel date (dd/mm/yyyy):");
            string dateInput;
            DateTime travelDate;
            while (true)
            {
                dateInput = Console.ReadLine();
                if (
                    !DateTime.TryParseExact(
                        dateInput,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out travelDate
                    )
                )
                {
                    Console.WriteLine(
                        "Invalid date format. Please enter the date in the format dd/MM/yyyy."
                    );
                    Console.WriteLine("For example, enter 13/06/2023 for June 13, 2023.");
                    Thread.Sleep(200);
                    continue;
                }
                else
                {
                    break;
                }
            }

            var filteredFlights = FlightLogic.GetFlightsByDestinationAndDate(
                selectedDestination,
                travelDate
            );

            if (filteredFlights.Count == 0)
            {
                Console.WriteLine("There are no available flights on the selected travel date...");
                Thread.Sleep(200);
                return;
            }

            Console.WriteLine(
                $"Available flights to {selectedDestination} on {travelDate.ToShortDateString()}:"
            );
            for (var i = 0; i < filteredFlights.Count; i++)
            {
                var flight = filteredFlights[i];
                Console.WriteLine($"{i + 1}. Flight {flight.FlightNumber}");
                Console.WriteLine($"   Departure: {flight.DepartureTime}");
                Console.WriteLine($"   Price: {flight.Price:C2}");
                Console.WriteLine();
            }

            Console.WriteLine("Enter the number corresponding to the flight you want to book:");
            var flightInput = Console.ReadLine();
            if (
                !int.TryParse(flightInput, out var flightIndex)
                || flightIndex < 1
                || flightIndex > filteredFlights.Count
            )
            {
                Console.WriteLine("Invalid input, please try again...");
                Thread.Sleep(200);
                return;
            }

            var selectedFlight = filteredFlights[flightIndex - 1];
            cost.FlightPrice = selectedFlight.Price;
            var flightNum = selectedFlight.FlightNumber;

            Console.WriteLine("For how many people would you like to book?");
            var peopleCount = Console.ReadLine();
            var peopleInt = 0;
            if (string.IsNullOrEmpty(peopleCount) || !int.TryParse(peopleCount, out peopleInt))
            {
                Console.WriteLine("Wrong input...");
                Console.ReadKey();
                return;
            }

            cost.FlightPrice = selectedFlight.Price;

            Console.WriteLine("Would you like to add catering to your booking? (y/n)");
            var answer = Console.ReadLine()?.ToLower();
            if (answer == "y")
            {
                var cateringList = CateringDataAccess.GetCatering();
                Console.WriteLine("Please choose from the following menu:");

                for (var i = 0; i < cateringList.Count; i++)
                {
                    var cateringItem = cateringList[i];
                    Console.WriteLine($"{i + 1}. {cateringItem.Name}");
                    Console.WriteLine($"   Description: {cateringItem.Description}");
                    Console.WriteLine($"   Price: {cateringItem.Price:C2}");
                    Console.WriteLine();
                }

                var choices = new List<int>();
                bool exitMenu = false;
                bool invalidInput = true;

                do
                {
                    invalidInput = false;
                    Console.WriteLine(
                        "Enter the number of the catering item you want to order (or 'q' to exit):"
                    );
                    var choiceInput = Console.ReadLine();

                    if (choiceInput.ToLower() == "q")
                    {
                        Console.WriteLine("Are you sure you want to exit the catering menu? (y/n)");
                        var confirmation = Console.ReadLine()?.ToLower();

                        if (confirmation == "y")
                        {
                            exitMenu = true;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    int choice;
                    if (!int.TryParse(choiceInput, out choice) || choice < 1 || choice > cateringList.Count)
                    {
                        invalidInput = true;
                        Console.WriteLine("Invalid input, please try again...");
                        Thread.Sleep(200);
                    }
                    else
                    {
                        choices.Add(choice - 1);
                    }
                } while (invalidInput || choices.Count < cateringList.Count);

                if (exitMenu)
                {
                    Console.WriteLine("Exiting catering menu...");
                    Thread.Sleep(200);
                }
                else
                {
                    var cateringItems = new List<Catering>();
                    var totalPrice = 0M;

                    foreach (var choice in choices)
                    {
                        var cateringItem = cateringList[choice];
                        cateringItems.Add(cateringItem);
                        totalPrice += cateringItem.Price;
                        cost.Catering += cateringItem.Price;
                    }

                    Console.WriteLine("Catering items ordered: ");
                    foreach (var cateringItem in cateringItems)
                    {
                        Console.WriteLine($"- {cateringItem.Name}");
                    }

                    Console.WriteLine($"Total price: {totalPrice:C2}");
                    Thread.Sleep(200);
                }

                var seats = new Dictionary<string, Person>();
                for (var i = 0; i < peopleInt; i++)
                {
                    Console.WriteLine($"What is the name of person #{i + 1}?");
                    var name = Console.ReadLine();
                    Console.WriteLine("What is the birthdate of this person? (Format: dd-MM-yyyy)");
                    var birthdateInput = Console.ReadLine();
                    if (
                        !DateTime.TryParseExact(
                            birthdateInput,
                            "dd-MM-yyyy",
                            null,
                            System.Globalization.DateTimeStyles.None,
                            out var birthdate
                        )
                    )
                    {
                        Console.WriteLine("Invalid date entered, please try again...");
                        Thread.Sleep(200);
                        Console.Clear();
                        i--;
                        continue;
                    }

                    Console.WriteLine(
                        "What is the ID, driver's license, or passport document number of this person?"
                    );
                    var docNum = Console.ReadLine();
                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(docNum))
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
                    seats.Add(seat, new Person(name, birthdate, docNum));
                    var yearDelta = (DateTime.Now - birthdate).TotalDays / 365.2425;
                    cost.SeatPrices.Add(
                        selectedFlight.Seats.First(h => h.Number == seat).Price
                            * (
                                yearDelta < 3
                                    ? 0m
                                    : yearDelta < 12
                                        ? 0.35m
                                        : yearDelta < 18
                                            ? 0.75m
                                            : 1.0m
                            )
                    );
                    Console.WriteLine("Successfully added another person to the booking.");
                    Thread.Sleep(200);
                }

                Console.WriteLine("The cost of this booking is as follows:");
                Console.WriteLine(cost);
                Console.ReadKey();

                var reservationNum = BookingLogic.GenerateUniqueReservationCode();
                BookingDataAccess.CreateBooking(new Booking(reservationNum, flightNum, seats, cost));
            }
        }
    }
}
