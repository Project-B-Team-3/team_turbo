using System;
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
                    $"View all available flights\n[2] Change seat pricing \n[3] Change catering pricing\n[4] Change catering items\n" +
                    $"[5] Select Ticket To Change Ticket\n[6] Quit");
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
                        Console.WriteLine("Change catering pricing");
                        break;

                    case ConsoleKey.D4:
                        Console.WriteLine("Change catering items");
                        break;

                    case ConsoleKey.D5:
                        Console.WriteLine("Change ticket pricing");
                        SelectFlightToUpdate();
                        break;

                    case ConsoleKey.D6:
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
                ConsoleKeyInfo sortChoice;
                sortChoice = Console.ReadKey(true);
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
                    flights.Sort((f1, f2) => f1.FlightNumber.CompareTo(f2.FlightNumber));
                    break;

                case "DepartureCity":
                    flights.Sort((f1, f2) => f1.DepartureCity.CompareTo(f2.DepartureCity));
                    break;

                case "DestinationCity":
                    flights.Sort((f1, f2) => f1.DestinationCity.CompareTo(f2.DestinationCity));
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

            Flight flightToUpdate = BookingLogic.GetFlightByNumber(flightNumber);

            if (flightToUpdate == null)
            {
                Console.WriteLine($"Flight with number {flightNumber} not found.");
                Console.ReadKey();
                return;
            }

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
                        Console.WriteLine("Quitting update flight menu.");
                        return;

                    default:
                        Console.WriteLine("Invalid option selected.");
                        break;
                }
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
            string newDestination = Console.ReadLine();

            flightToUpdate.DestinationCity = newDestination;
            FlightDataAccess.UpdateFlight(flightToUpdate);

            Console.WriteLine($"Flight {flightToUpdate.FlightNumber} updated.");
            Console.ReadKey();
        }
    }
}