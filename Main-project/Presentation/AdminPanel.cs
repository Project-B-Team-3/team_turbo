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
                    $"[5] Change Ticket pricing\n[6] Change Ticket Destination\n[7] Quit");
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
                        ChangeTicketPrice();
                        break;

                    case ConsoleKey.D6:
                        Console.WriteLine("Change Ticket Destination");
                        ChangeFlightsDestination();
                        break;

                    case ConsoleKey.D7:
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
            foreach (var flight in BookingLogic.GetAllFlights())
            {
                Console.WriteLine($"Flight number: {flight.FlightNumber}");
                Console.WriteLine($"Departure: {flight.DepartureCity}");
                Console.WriteLine($"Destination: {flight.DestinationCity}");
                Console.WriteLine($"Departure time: {flight.DepartureTime}");
                Console.WriteLine($"Seats available: {flight.Seats.Count}");
                Console.WriteLine($"Price: {flight.Price}");
                Console.WriteLine();
            }
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

            Console.WriteLine($"What do you wish to do?\n[1] Change ticket pricing\n[2] Change ticket destination");
            ConsoleKeyInfo adminChoice;
            adminChoice = Console.ReadKey(true);

            switch (adminChoice.Key)
            {
                case ConsoleKey.D1:
                    ChangeTicketPrice();
                    break;

                case ConsoleKey.D2:
                    ChangeFlightsDestination();
                    break;

                default:
                    Console.WriteLine("Invalid option selected.");
                    Console.ReadKey();
                    break;
            }
        }

        public static void ChangeTicketPrice()
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

        public static void ChangeFlightsDestination()
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