using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Logic;
using System.Globalization;

namespace Main_project.Presentation;

public static class CreateBooking
{
    public static void CreateNewBooking()
    {
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

        int destinationIndex;
        var isValidInput = false;

        do
        {
            var destinationInput = Console.ReadLine();

            if (
                !int.TryParse(destinationInput, out destinationIndex)
                || destinationIndex < 1
                || destinationIndex > destinations.Count
            )
                Console.WriteLine("Invalid input, please choose a valid destination number.");
            else
                isValidInput = true;
        } while (!isValidInput);

        var selectedDestination = destinations[destinationIndex - 1];

        var travelDate = GetValidTravelDate();

        var flights = FlightDataAccess.GetFlightsByDestination(selectedDestination);
        List<Flight> filteredFlights = FlightLogic.GetFlightsByDestinationAndDate(
            selectedDestination,
            travelDate
        );

        if (filteredFlights.Count == 0)
        {
            Console.WriteLine("There are no available flights on the selected travel date.");
            Console.WriteLine("Do you want to choose a different travel date? (y/n)");

            var choice = Console.ReadLine()?.ToLower();

            while (choice != "y" && choice != "n")
            {
                Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
                choice = Console.ReadLine()?.ToLower();
            }

            if (choice == "y")
            {
                travelDate = GetValidTravelDate();
                filteredFlights = FlightLogic.GetFlightsByDestinationAndDate(
                    selectedDestination,
                    travelDate
                );

                if (filteredFlights.Count == 0)
                {
                    Console.WriteLine(
                        "There are no available flights on the selected travel date."
                    );
                    Console.WriteLine("Returning to the main menu...");
                    Console.ReadKey(true);
                    return;
                }
            }
            else if (choice == "n")
            {
                Console.WriteLine("Returning to the main menu...");
                Console.ReadKey(true);
                return;
            }
        }

		Console.WriteLine(
			$"Available flights to {selectedDestination} on {travelDate.ToShortDateString()}:"
		);
		for (var i = 0; i < filteredFlights.Count; i++)
		{
			var flight = filteredFlights[i];
			Console.WriteLine($"{i + 1}. Flight {flight.FlightNumber}");
			Console.WriteLine(
				$"   Departure: {flight.DepartureCity} ({flight.DepartureTime.ToString("HH:mm", CultureInfo.InvariantCulture)})"
			);
			Console.WriteLine($"   Destination: {flight.DestinationCity}");
			Console.WriteLine($"   Seats Available: {flight.Seats.Count(s => s.Available)}");
			Console.WriteLine($"   Price of the flight: {flight.Price}");
			Console.WriteLine();
		}

        Console.WriteLine("Enter the number corresponding to your desired flight:");

        int flightIndex;
        isValidInput = false;

        do
        {
            var flightInput = Console.ReadLine();

            if (
                !int.TryParse(flightInput, out flightIndex)
                || flightIndex < 1
                || flightIndex > filteredFlights.Count
            )
                Console.WriteLine("Invalid input, please choose a valid flight number.");
            else
                isValidInput = true;
        } while (!isValidInput);

        var selectedFlight = filteredFlights[flightIndex - 1];
        Console.WriteLine(selectedFlight);
        var flightNum = selectedFlight.FlightNumber;

        Console.WriteLine("Enter the number of passengers:");

        int numberOfPassengers;
        isValidInput = false;

        do
        {
            var passengersInput = Console.ReadLine();

            if (!int.TryParse(passengersInput, out numberOfPassengers) || numberOfPassengers <= 0)
                Console.WriteLine("Invalid input, please enter a valid number of passengers.");
            else
                isValidInput = true;
        } while (!isValidInput);

        var cost = new Cost(selectedFlight.Price, 0, new List<decimal>());
        var totalPrice = FlightLogic.CalculateTotalPrice(selectedFlight, numberOfPassengers);
        cost.FlightPrice = totalPrice;

        Console.WriteLine($"Total price for {numberOfPassengers} passengers: {totalPrice:C}");
        Console.WriteLine("Do you want to order catering services? (y/n)");

        var answer = Console.ReadLine()?.ToLower();

        while (answer != "y" && answer != "n")
        {
            Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
            answer = Console.ReadLine()?.ToLower();
        }

        if (answer == "y")
        {
            var cateringItems = CateringDataAccess.GetCatering();
            var exitCateringMenu = false;
            var chosenCateringItems = new List<Catering>();

            while (!exitCateringMenu)
            {
                Console.WriteLine();
                Console.WriteLine("Catering Menu:");
                Console.WriteLine("==============");

                for (var i = 0; i < cateringItems.Count; i++)
                {
                    var cateringItem = cateringItems[i];
                    Console.WriteLine($"{i + 1}. {cateringItem.Name}");
                    Console.WriteLine($"   Description: {cateringItem.Description}");
                    Console.WriteLine($"   Price: {cateringItem.Price:C2}");
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine(
                    "Enter the number of the catering item you want to order (or 'q' to exit):"
                );

                var cateringInput = Console.ReadLine()?.ToLower();

                if (cateringInput == "q")
                {
                    Console.WriteLine("Are you sure you want to exit the catering menu? (y/n)");

                    var exitChoice = Console.ReadLine()?.ToLower();

                    while (exitChoice != "y" && exitChoice != "n")
                    {
                        Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
                        exitChoice = Console.ReadLine()?.ToLower();
                    }

                    if (exitChoice == "y")
                        exitCateringMenu = true;
                }
                else
                {
                    int cateringIndex;
                    if (
                        !int.TryParse(cateringInput, out cateringIndex)
                        || cateringIndex < 1
                        || cateringIndex > cateringItems.Count
                    )
                    {
                        Console.WriteLine(
                            "Invalid input, please choose a valid catering item number."
                        );
                    }
                    else
                    {
                        var selectedCateringItem = cateringItems[cateringIndex - 1];
                        chosenCateringItems.Add(selectedCateringItem);
                        Console.WriteLine("Catering item added to the order.");
                    }
                }
            }

            if (chosenCateringItems.Count > 0)
            {
                Console.WriteLine("Catering items ordered:");
                decimal totalPrice_catering = 0;

                foreach (var cateringItem in chosenCateringItems)
                {
                    Console.WriteLine($"- {cateringItem.Name}");
                    totalPrice_catering += cateringItem.Price;
                    cost.Catering += cateringItem.Price;
                }

                Console.WriteLine($"Total price: {totalPrice_catering:C2}");
            }
            else
            {
                Console.WriteLine("No catering items were ordered.");
            }
        }

        var availableSeats = selectedFlight.Seats.Where(seat => seat.Available).ToList();

        if (availableSeats.Count < numberOfPassengers)
        {
            Console.WriteLine("Insufficient seats available for the requested number of people.");
            return;
        }

        Dictionary<string, Person> seats = new();

        for (var i = 0; i < numberOfPassengers; i++)
        {
            Console.WriteLine($"Enter the details for passenger #{i + 1}:");
            Console.Write("Name: ");
            var name = Console.ReadLine();
            Console.Write("Birthdate (dd-MM-yyyy): ");
            DateTime birthdate;

            while (
                !DateTime.TryParseExact(
                    Console.ReadLine(),
                    "dd-MM-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out birthdate
                )
                || birthdate.Year <= 1910
            )
                if (birthdate.Year <= 1910)
                    Console.WriteLine("Invalid birthdate. Please enter a valid birthdate.");
                else
                    Console.WriteLine(
                        "Invalid input. Please enter a valid birthdate in the format dd-MM-yyyy."
                    );

            Console.Write("Document number: ");
            var documentNum = Console.ReadLine();

            // Perform validation for the document number
            while (string.IsNullOrEmpty(documentNum))
            {
                Console.WriteLine("Invalid input. Please enter a valid document number.");
                Console.Write("Document number: ");
                documentNum = Console.ReadLine();
            }

            var seat = SeatSelector.SelectSeat(flightNum);
            SeatLogic.UpdateSeat(flightNum, seat, false);
            seats.Add(seat, new Person(name, birthdate, documentNum));
            var yearDelta = (DateTime.Now - birthdate).TotalDays / 365.2425;
            cost.SeatPrices.Add(
                FlightDataAccess
                    .GetFlights()
                    .First(h => h.FlightNumber == flightNum)
                    .Seats.First(h => h.Number == seat)
                    .Price
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
        }

        var reservationNumber = BookingLogic.GenerateUniqueReservationCode();

        var booking = new Booking(reservationNumber, flightNum, seats, cost);

        foreach (var line in booking.GetLines())
            Console.WriteLine(line);

        Console.WriteLine("Confirm booking? (y/n)");
        var confirmation = Console.ReadLine()?.ToLower();

        while (confirmation != "y" && confirmation != "n")
        {
            Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
            confirmation = Console.ReadLine()?.ToLower();
        }

        if (confirmation == "y")
        {
            BookingDataAccess.CreateBooking(booking);
            Console.WriteLine("Booking confirmed!");

            booking.GetLines().ForEach(Console.WriteLine);
            Console.WriteLine(
                "Please write down your reservation number. You will need this to cancel your booking."
            );
        }
        else if (confirmation == "n")
        {
            foreach (var seatsKey in seats.Keys)
                SeatLogic.UpdateSeat(flightNum, seatsKey, true);
            Console.WriteLine("Booking cancelled.");
        }

        Console.ReadKey(true);
    }

    private static DateTime GetValidTravelDate()
    {
        DateTime travelDate;
        var isValidDate = false;

        do
        {
            Console.WriteLine("Enter the travel date (dd-MM-yyyy):");
            var travelDateInput = Console.ReadLine();
            if (
                !DateTime.TryParseExact(
                    travelDateInput,
                    "dd-MM-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out travelDate
                )
            )
                Console.WriteLine(
                    "Invalid date format. Please enter the date in the specified format."
                );
            else if (travelDate.Date < DateTime.Today.Date)
                Console.WriteLine("Travel date must be in the future. Please enter a valid date.");
            else
                isValidDate = true;
        } while (!isValidDate);

        return travelDate;
    }
}
