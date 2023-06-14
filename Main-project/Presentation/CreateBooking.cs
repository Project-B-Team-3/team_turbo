using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Logic;
using System.Globalization;

namespace Main_project.Presentation
{
    public static class CreateBooking
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

            int destinationIndex;
            bool isValidInput = false;

            do
            {
                var destinationInput = Console.ReadLine();

                if (
                    !int.TryParse(destinationInput, out destinationIndex)
                    || destinationIndex < 1
                    || destinationIndex > destinations.Count
                )
                {
                    Console.WriteLine("Invalid input, please choose a valid destination number.");
                }
                else
                {
                    isValidInput = true;
                }
            } while (!isValidInput);

            var selectedDestination = destinations[destinationIndex - 1];

            DateTime travelDate = GetValidTravelDate();

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
                        Console.ReadKey(intercept: true);
                        return;
                    }
                }
                else if (choice == "n")
                {
                    Console.WriteLine("Returning to the main menu...");
                    Console.ReadKey(intercept: true);
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
                {
                    Console.WriteLine("Invalid input, please choose a valid flight number.");
                }
                else
                {
                    isValidInput = true;
                }
            } while (!isValidInput);

            var selectedFlight = filteredFlights[flightIndex - 1];

            Console.WriteLine("Enter the number of passengers:");

            int numberOfPassengers;
            isValidInput = false;

            do
            {
                var passengersInput = Console.ReadLine();

                if (
                    !int.TryParse(passengersInput, out numberOfPassengers)
                    || numberOfPassengers <= 0
                )
                {
                    Console.WriteLine("Invalid input, please enter a valid number of passengers.");
                }
                else
                {
                    isValidInput = true;
                }
            } while (!isValidInput);

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
                bool exitCateringMenu = false;
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
                            Console.WriteLine(
                                "Invalid input. Please enter 'y' for yes or 'n' for no."
                            );
                            exitChoice = Console.ReadLine()?.ToLower();
                        }

                        if (exitChoice == "y")
                        {
                            exitCateringMenu = true;
                        }
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
            var selectedSeats = availableSeats.Take(numberOfPassengers).ToList();

            if (selectedSeats.Count < numberOfPassengers)
            {
                Console.WriteLine(
                    "Insufficient seats available for the requested number of people."
                );
                return;
            }

            foreach (var seat in selectedSeats)
            {
                seat.Available = false;
            }

            var passengers = new List<Person>();

            for (var i = 0; i < numberOfPassengers; i++)
            {
                Console.WriteLine($"Enter the details for passenger #{i + 1}:");
                Console.Write("Name: ");
                var name = Console.ReadLine();
                Console.Write("Birthdate (dd/MM/yyyy): ");
                DateTime birthdate;

                while (
                    !DateTime.TryParseExact(
                        Console.ReadLine(),
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out birthdate
                    )
                    || birthdate.Year <= 1910
                )
                {
                    if (birthdate.Year <= 1910)
                    {
                        Console.WriteLine("Invalid birthdate. Please enter a valid birthdate.");
                    }
                    else
                    {
                        Console.WriteLine(
                            "Invalid input. Please enter a valid birthdate in the format dd/MM/yyyy."
                        );
                    }
                }

                Console.Write("Document number: ");
                var documentNum = Console.ReadLine();

                // Perform validation for the document number
                while (string.IsNullOrEmpty(documentNum))
                {
                    Console.WriteLine("Invalid input. Please enter a valid document number.");
                    Console.Write("Document number: ");
                    documentNum = Console.ReadLine();
                }

                passengers.Add(new Person(name, birthdate, documentNum));

                Console.WriteLine("Confirm booking? (y/n)");
                var confirmation = Console.ReadLine()?.ToLower();

                while (confirmation != "y" && confirmation != "n")
                {
                    Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
                    confirmation = Console.ReadLine()?.ToLower();
                }

                if (confirmation == "y")
                {
                    var flightNumber = selectedFlight.FlightNumber;
                    var passengersDictionary = passengers.ToDictionary(p => p.DocumentNum);
                    var seats = selectedSeats.ToDictionary(s => s.Number, s => (Person)null); // Placeholder value for seats
                    var reservationNumber = BookingLogic.GenerateUniqueReservationCode();

                    var booking = new Booking(
                        reservationNumber,
                        flightNumber,
                        seats,
                        new Cost(
                            selectedFlight.Price,
                            0, // Replace `cateringPrice` with the actual value or set it to 0
                            selectedSeats.Select(s => s.Price).ToList()
                        )
                    );

                    BookingDataAccess.CreateBooking(booking);

                    Console.WriteLine(
                        $"You just booked a flight from {selectedFlight.DepartureCity} to {selectedFlight.DestinationCity}."
                    );
                    Console.WriteLine("Booking Details");
                    Console.WriteLine($"Reservation Number: {booking.ReservationNumber}");
                    Console.WriteLine($"Flight Number: {booking.FlightNumber}");
                    Console.WriteLine(
                        $"Departure Airport Code: {selectedFlight.DepartureAirportCode}"
                    );
                    Console.WriteLine(
                        $"Destination Airport Code: {selectedFlight.DestinationAirportCode}"
                    );
                    Console.WriteLine(
                        $"Departure Time: {selectedFlight.DepartureTime:dd-M-yyy HH:mm:ss}"
                    );
                    Console.WriteLine($"For the total price of: {booking.Cost.GetTotal()}");
                    Console.WriteLine();
                    Console.WriteLine("And your seats are:");
                    foreach (var person in seats.Values.ToArray())
                    {
                        Console.WriteLine(person.ToString());
                    }

                    Console.WriteLine("Booking confirmed!");
                    Console.WriteLine("Total cost: {0:C2}", booking.Cost.GetTotal());
                }
                else if (confirmation == "n")
                {
                    Console.WriteLine("Booking cancelled.");
                }

                Console.ReadKey(intercept: true);
            }
        }

        private static DateTime GetValidTravelDate()
        {
            DateTime travelDate;
            bool isValidDate = false;

            do
            {
                Console.WriteLine("Enter the travel date (dd/MM/yyyy):");
                var travelDateInput = Console.ReadLine();
                if (
                    !DateTime.TryParseExact(
                        travelDateInput,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out travelDate
                    )
                )
                {
                    Console.WriteLine(
                        "Invalid date format. Please enter the date in the specified format."
                    );
                }
                else if (travelDate.Date < DateTime.Today.Date)
                {
                    Console.WriteLine(
                        "Travel date must be in the future. Please enter a valid date."
                    );
                }
                else
                {
                    isValidDate = true;
                }
            } while (!isValidDate);

            return travelDate;
        }
    }
}
