using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Logic;

namespace Main_project.Presentation
{
    public static class AdminPanel
    {
        public static void Admin()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Admin Panel");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"What do you wish to do?\n[1] " +
                    $"View all available flights\n[2] Change seat pricing \n[3] Change catering \n" +
                    $"[4] Select Ticket To Change Ticket\n[5] Quit");
                ConsoleKeyInfo Adminchoice;
                Adminchoice = Console.ReadKey(true);
                switch (Adminchoice.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Viewing all available flights");
                        DisplayAllFlights();
                        break;

                    case ConsoleKey.D2:
                        Console.WriteLine("Change seat pricing");
                        break;

                    case ConsoleKey.D3:
                        Console.WriteLine("Change catering");
                        SelectCateringToUpdate();
                        break;

                    case ConsoleKey.D4:
                        Console.WriteLine("Change/Cancel flights");
                        SelectFlightToUpdate();
                        break;

                    case ConsoleKey.D5:
                        Console.WriteLine("Program has been quit");
                        return;

                    default:
                        // Checks for capslock/numlock
                        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                        {
                            if (Console.CapsLock && Console.NumberLock)
                            {
                                Console.WriteLine(Adminchoice.KeyChar);
                                Console.Write("Invalid option");
                            }
                        }
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
            }
        }
        
        public static void DisplayAllFlights()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("View all available flights");
                Console.WriteLine($"Sort by:\n[1] Flight number\n[2] Departure city\n[3] Destination city\n[4] Departure time\n[5] Seats available\n[6] Price\n[7] Quit");
                var sortChoice = Console.ReadKey(true);
                switch (sortChoice.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Sorting by Flight number");
                        SortAndDisplayFlights("FlightNumber");
                        break;

                    case ConsoleKey.D2:
                        Console.WriteLine("Sorting by Departure city");
                        SortAndDisplayFlights("DepartureCity");
                        break;

                    case ConsoleKey.D3:
                        Console.WriteLine("Sorting by Destination city");
                        SortAndDisplayFlights("DestinationCity");
                        break;

                    case ConsoleKey.D4:
                        Console.WriteLine("Sorting by Departure time");
                        SortAndDisplayFlights("DepartureTime");
                        break;

                    case ConsoleKey.D5:
                        Console.WriteLine("Sorting by Seats available");
                        SortAndDisplayFlights("Seats.Count");
                        break;

                    case ConsoleKey.D6:
                        Console.WriteLine("Sorting by Price");
                        SortAndDisplayFlights("Price");
                        break;

                    case ConsoleKey.D7:
                        Console.WriteLine("Returning to Admin Panel");
                        return;

                    default:
                        Console.WriteLine("Invalid option selected.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
            }
        }

        public static void SortAndDisplayFlights(string sortBy)
        {
            List<Flight> flights = BookingLogic.GetAllFlights();
            switch (sortBy)
            {
                case "FlightNumber":
                    flights.Sort((f1, f2) => String.Compare(f1.FlightNumber, f2.FlightNumber, StringComparison.Ordinal));
                    break;

                case "DepartureCity":
                    flights.Sort((f1, f2) => String.Compare(f1.DepartureCity, f2.DepartureCity, StringComparison.Ordinal));
                    break;

                case "DestinationCity":
                    flights.Sort((f1, f2) => String.Compare(f1.DestinationCity, f2.DestinationCity, StringComparison.Ordinal));
                    break;

                case "DepartureTime":
                    flights.Sort((f1, f2) => f1.DepartureTime.CompareTo(f2.DepartureTime));
                    break;

                case "Seats.Count":
                    flights.Sort((f1, f2) => f1.Seats.Count.CompareTo(f2.Seats.Count));
                    break;

                case "Price":
                    flights.Sort((f1, f2) => f1.Price.CompareTo(f2.Price));
                    break;

                default:
                    Console.WriteLine("Invalid option selected.");
                    return;
            }

            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine("| Flight number   | Departure  | Destination  | Departure time       | Seats available  | Price     |");
            Console.WriteLine("-----------------------------------------------------------------------------");
            foreach (var flight in flights)
            {
                Console.WriteLine($"| {flight.FlightNumber,-15} | {flight.DepartureCity,-10} | {flight.DestinationCity,-12} | {flight.DepartureTime,-20} | {flight.Seats.Count,-16} | {flight.Price,-9} |");
            }
            Console.WriteLine("-----------------------------------------------------------------------------");
        }


        public static void SelectFlightToUpdate()
        {
            Console.Write("Enter the flight number to update: ");
            string flightNumber = Console.ReadLine();

            if (flightNumber == null)
            {
                Console.WriteLine("Null value entered");
                Console.ReadKey();
                return;
            }

            if (BookingLogic.GetAllFlights().All(h => h.FlightNumber != flightNumber))
            {
                Console.WriteLine($"Flight with number {flightNumber} not found.");
                Console.ReadKey();
                return;
            }
            Flight flightToUpdate = BookingLogic.GetFlightByNumber(flightNumber);

            Console.WriteLine($"Selected flight: {flightToUpdate.FlightNumber} - {flightToUpdate.DepartureCity} to {flightToUpdate.DestinationCity}");

            while (true)
            {
                Console.WriteLine($"What do you wish to do?\n[1] Change ticket pricing\n[2] Change ticket destination\n[3] Quit");
                ConsoleKeyInfo adminChoice = Console.ReadKey(true);

                switch (adminChoice.Key)
                {
                    case ConsoleKey.D1:
                        ChangeTicketPrice(flightToUpdate);
                        break;

                    case ConsoleKey.D2:
                        ChangeFlightsDestination(flightToUpdate);
                        break;
                    
                    case ConsoleKey.D3:
                        Console.WriteLine("Cancel flight");
                        CancelFlight(flightToUpdate);
                        break;

                    case ConsoleKey.D4:
                        Console.WriteLine("Quitting update flight menu.");
                        return;

                    default:
                        Console.WriteLine("Invalid option selected.");
                        break;
                }
            }
        }
        private static void SelectCateringToUpdate()
        {
            Console.Clear();
            
            Console.WriteLine("Right now the list of caterings is:");

            List<Catering> caterings = CateringLogic.cateringList();
            
            foreach (var catering in caterings)
            {
                Console.WriteLine($"{catering.Name}, {catering.Description}, {catering.Price}, {catering.IsHalal}");
            }
            
            Console.WriteLine("Select Option:");
            Console.WriteLine("[1] Change Caterings");
            Console.WriteLine("[2] Change Caterings");

            ConsoleKeyInfo input = Console.ReadKey(true);

            switch (input.Key)
            {
                case ConsoleKey.D1:
                    AddCatering();
                    break;

                case ConsoleKey.D2:
                    ChangeCaterings();
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        public static void AddCatering()
        {
            Console.Clear();
            Console.WriteLine("Add New Catering Option:");
            
            List<Catering> caterings = CateringLogic.cateringList();
            
            foreach (var catering in caterings)
            {
                Console.WriteLine($"Name: {catering.Name}Description: {catering.Description}Price: {catering.Price}IsHalal: {catering.IsHalal}");
            }

            Console.WriteLine("Enter the name of the catering:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the description of the catering:");
            string description = Console.ReadLine();

            Console.WriteLine("Enter the price of the catering:");
            if (decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Is the catering halal? (y/n)");
                bool isHalal = Console.ReadLine()?.ToLower() == "y";

                Catering newCatering = new Catering(name, description, price, isHalal);
                caterings.Add(newCatering);

                Console.WriteLine("\nNew catering option added successfully!");
            }
            else
            {
                Console.WriteLine("\nInvalid price input. Catering option not added.");
            }
        }

        private static void ChangeCaterings()
        {
            Console.Clear();
            Console.WriteLine("Select Catering to Update:");
    
            List<Catering> caterings = CateringLogic.cateringList();
    
            for (int i = 0; i < caterings.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {caterings[i].Name}");
            }
    
            Console.WriteLine("\nEnter the number of the catering to update:");
            string input = Console.ReadLine();
    
            if (int.TryParse(input, out int cateringIndex) && cateringIndex >= 1 && cateringIndex <= caterings.Count)
            {
                Catering selectedCatering = caterings[cateringIndex - 1];
        
                Console.WriteLine(
                    $"\nUpdating Catering: Name: {selectedCatering.Name} Description: {selectedCatering.Description} Price: {selectedCatering.Price} Is Halal: {selectedCatering.IsHalal}");


                Console.WriteLine("\nEnter the new name:");
                string newName = Console.ReadLine();
                selectedCatering.Name = newName;
                
                Console.WriteLine("\nEnter the new description:");
                string newDescription = Console.ReadLine();
                selectedCatering.Name = newDescription;
                
                Console.WriteLine("\nEnter the new price:");
                string priceInput = Console.ReadLine();
                if (decimal.TryParse(priceInput, out decimal newPrice))
                {
                    selectedCatering.Price = newPrice;
                    Console.WriteLine("Catering price updated successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid price input. Catering price not updated.");
                }
                
                Console.WriteLine("Is the catering halal? (y/n):");
                string halalInput = Console.ReadLine();
                bool newIsHalal = (halalInput?.ToLower() == "y");
                selectedCatering.IsHalal = newIsHalal;
        
                // Repeat the above steps for other properties if needed
        
                Console.WriteLine("\nCatering updated successfully!");
            }
            else
            {
                Console.WriteLine("\nInvalid input or catering not found.");
            }
        }


        public static void ChangeTicketPrice(Flight flightToUpdate)
        {
            
            Console.WriteLine($"Current ticket price for flight {flightToUpdate.FlightNumber}: {flightToUpdate.Price}");

            Console.Write("Enter the new ticket price: ");
            double newPrice;
            if (!double.TryParse(Console.ReadLine(), out newPrice))
            {
                Console.WriteLine("Invalid price entered.");
                Console.ReadKey();
                return;
            }

            flightToUpdate.Price = (decimal)newPrice;
            FlightDataAccess.UpdateFlight(flightToUpdate);

            Console.WriteLine(
                $"Ticket price for flight {flightToUpdate.FlightNumber} updated to {flightToUpdate.Price}.");
            Console.ReadKey();
        }

        public static void ChangeFlightsDestination(Flight flightToUpdate)
        {

            Console.WriteLine($"Current flight details: {flightToUpdate}");

            Console.Write("Enter the new destination city: ");
            string? newDestination = Console.ReadLine();
            if (newDestination is null)
            {
                Console.WriteLine("Null value entered.");
                Console.ReadKey();
                return;
            }

            flightToUpdate.DestinationCity = newDestination;
            FlightDataAccess.UpdateFlight(flightToUpdate);

            Console.WriteLine($"Flight {flightToUpdate.FlightNumber} updated.");
            Console.ReadKey();
        }
        public static void CancelFlight(Flight flightToUpdate)
        {
            Console.WriteLine($"Selected flight: {flightToUpdate.FlightNumber} - {flightToUpdate.DepartureCity} to {flightToUpdate.DestinationCity}");

            Console.Write("Are you sure you want to cancel this flight? (Y/N): ");
            ConsoleKeyInfo confirmChoice = Console.ReadKey(true);
            if (confirmChoice.Key == ConsoleKey.Y)
            {
                FlightDataAccess.DeleteFlight(flightToUpdate);
                Console.WriteLine($"Flight {flightToUpdate.FlightNumber} has been canceled.");
            }
            else
            {
                Console.WriteLine("Flight cancellation aborted.");
            }

            Console.ReadKey();
        }
    }
}