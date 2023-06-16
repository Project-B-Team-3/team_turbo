using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Logic;

namespace Main_project.Presentation;

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
			Console.WriteLine("What do you wish to do?\n" +
			                  "[1] View all available flights\n" +
			                  "[2] Update catering\n" +
			                  "[3] Change or cancel flights\n" +
			                  "[4] Quit");
			ConsoleKeyInfo Adminchoice;
			// Break the loop if a valid option is selected
			Adminchoice = Console.ReadKey(true);
			switch (Adminchoice.Key)
			{
				case ConsoleKey.D1:
					Console.WriteLine("Viewing all available flights");
					DisplayAllFlights();
					break;

				case ConsoleKey.D2:
					Console.WriteLine("Update catering");
					SelectCateringToUpdate();
					break;

				case ConsoleKey.D3:
					Console.WriteLine("Change or cancel flights");
					SelectFlightToUpdate();
					break;

				case ConsoleKey.D4:
					Console.WriteLine("Exiting the admin menu...");
					return;

				default:
					// Checks for capslock/numlock
					if (Environment.OSVersion.Platform == PlatformID.Win32NT)
						if (Console.CapsLock && Console.NumberLock)
						{
							Console.WriteLine(Adminchoice.KeyChar);
							Console.Write("Invalid option");
						}

					break;
			}

			Console.Clear();
		}
	}

	public static void SelectFlightToUpdate()
	{
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("Flight Update Panel\n");
		Console.ForegroundColor = ConsoleColor.White;

		Console.WriteLine("The list of the flight:");
		Console.WriteLine(
			"Sort flights by: [1] FlightNumber, [2] DepartureCity, [3] DestinationCity, [4] DepartureTime, [5] Seats.Count, [6] Price");
		string sortBy;
		while (true)
		{
			// Break the loop if a valid option is selected
			var sortChoice = Console.ReadKey(true);
			switch (sortChoice.Key)
			{
				case ConsoleKey.D1:
					sortBy = "FlightNumber";
					break;

				case ConsoleKey.D2:
					sortBy = "DepartureCity";
					break;

				case ConsoleKey.D3:
					sortBy = "DestinationCity";
					break;

				case ConsoleKey.D4:
					sortBy = "DepartureTime";
					break;

				case ConsoleKey.D5:
					sortBy = "Seats.Count";
					break;

				case ConsoleKey.D6:
					sortBy = "Price";
					break;

				default:
					Console.WriteLine("Invalid option selected. Please try again.");
					continue;
			}

			break; // Break the loop if a valid option is selected
		}

		SortAndDisplayFlights(sortBy, true);
		Console.WriteLine();

		string flightNumber = null;
		var isValidFlightNumber = false;

		while (!isValidFlightNumber)
		{
			Console.Write("Enter the flight number to update: ");
			flightNumber = Console.ReadLine();
			Console.Write("loading");

			if (flightNumber == null)
			{
				Console.Clear();
				Console.WriteLine("Null value entered");
				Console.ReadKey();
				return;
			}

			if (BookingLogic.GetAllFlights().Any(h => h.FlightNumber == flightNumber))
			{
				Console.Clear();
				isValidFlightNumber = true;
			}
			else
			{
				Console.Clear();
				Console.WriteLine($"Flight with number {flightNumber} not found.");
			}
		}

		var flightToUpdate = BookingLogic.GetFlightByNumber(flightNumber);
		Console.WriteLine();
		var line = new string('-', 100);
		Console.WriteLine(line);
		Console.WriteLine(
			$"| Flight number   | Departure  | Destination  | Departure time       | Seats available  | Price     |");
		Console.WriteLine(line);
		Console.WriteLine(
			$"| {flightToUpdate.FlightNumber,-15} | {flightToUpdate.DepartureCity,-10} | {flightToUpdate.DestinationCity,-12} | {flightToUpdate.DepartureTime,-20} | {flightToUpdate.Seats.Count,-16} | {flightToUpdate.Price,-9} |");
		Console.WriteLine(line);
		Console.WriteLine();
		while (true)
		{
			Console.WriteLine("What do you wish to do?\n" +
			                  "[1] Change ticket pricing\n" +
			                  "[2] Change ticket destination\n" +
			                  "[3] Cancel flight\n" +
			                  "[4] Return to Admin");

			var adminChoice = Console.ReadKey(true);

			switch (adminChoice.Key)
			{
				case ConsoleKey.D1:
					Console.WriteLine("Change ticket pricing");
					ChangeFlightsPrice(flightToUpdate);
					break;

				case ConsoleKey.D2:
					Console.WriteLine("Change ticket destination");
					ChangeFlightsDestination(flightToUpdate);
					break;

				case ConsoleKey.D3:
					Console.WriteLine("Cancel flight");
					CancelFlight(flightToUpdate);
					break;

				case ConsoleKey.D4:
					Console.WriteLine("Returning to Admin.");
					return;

				default:
					Console.WriteLine("Invalid option selected.");
					break;
			}
		}
	}

	public static void SelectCateringToUpdate()
	{
		while (true)
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Catering Update Panel\n");
			Console.ForegroundColor = ConsoleColor.White;

			Console.WriteLine("\nSelect an option:\n" +
			                  "[1] Show a list of the current catering items\n" +
			                  "[2] Add a new catering item\n" +
			                  "[3] Change a catering item\n" +
			                  "[4] Delete a catering item\n" +
			                  "[5] Return to Admin");


			var input = Console.ReadKey(true);

			switch (input.Key)
			{
				case ConsoleKey.D1:
					Console.WriteLine("The list of available caterings is:");
					DisplayCaterings();
					break;
				case ConsoleKey.D2:
					Console.WriteLine("Add a new catering item");
					AddCatering();
					break;

				case ConsoleKey.D3:
					Console.WriteLine("Change a catering item");
					ChangeCaterings();
					break;

				case ConsoleKey.D4:
					Console.WriteLine("Delete a catering item");
					DeleteCatering();
					break;

				case ConsoleKey.D5:
					Console.WriteLine("return to Admin.");
					return;

				default:
					Console.WriteLine("Invalid option.");
					break;
			}
		}
	}

	public static void DisplayCaterings()
	{
		var caterings = CateringDataAccess.GetCatering();
		var line = new string('-', 196);
		Console.WriteLine(line);
		Console.WriteLine($"| {"ID",-5} | {"Name",-30} | {"Description",-120} | {"Price",-10} | {"Is Halal",-10} |");
		Console.WriteLine(line);

		var id = 1;
		foreach (var catering in caterings)
		{
			Console.WriteLine(
				$"| {id++,5} | {catering.Name,-30} | {catering.Description,-120} | {catering.Price,-10} | {(catering.IsHalal ? "Yes" : "No"),-10} |");
			Console.WriteLine(line);
		}
	}


	public static void AddCatering()
	{
		Console.Clear();
		Console.WriteLine("Add New Catering Option:");

		DisplayCaterings();

		Console.WriteLine("Enter the name of the catering:");
		var name = Console.ReadLine();

		Console.WriteLine("Enter the description of the catering:");
		var description = Console.ReadLine();

		Console.WriteLine("Enter the price of the catering:");
		if (decimal.TryParse(Console.ReadLine(), out var price))
		{
			Console.WriteLine("Is the catering halal? (y/n)");
			var isHalal = Console.ReadLine()?.ToLower() == "y";

			var newCatering = new Catering(name, description, price, isHalal);
			CateringDataAccess.CreateCateringItem(newCatering);

			Console.WriteLine("\nNew catering option added successfully!");
		}
		else
		{
			Console.WriteLine("\nInvalid price input. Catering option not added.");
		}

		Console.WriteLine("Press any key to go back...");
		Console.ReadKey();
	}

	public static void DeleteCatering()
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("Select Catering to Delete:");

			var caterings = CateringDataAccess.GetCatering();
			DisplayCaterings();

			Console.WriteLine("\nEnter the number of the catering to delete (0 to exit):");
			var input = Console.ReadLine();

			if (int.TryParse(input, out var cateringIndex))
			{
				if (cateringIndex >= 1 && cateringIndex <= caterings.Count)
				{
					var selectedCatering = caterings[cateringIndex - 1];
					Console.WriteLine($"\nDeleting Catering: {selectedCatering.Name}");

					// Confirm deletion
					Console.WriteLine("Are you sure you want to delete this catering? (y/n):");
					var confirmInput = Console.ReadLine()?.ToLower();
					if (confirmInput == "y")
					{
						CateringDataAccess.DeleteCatering(selectedCatering);
						Console.WriteLine("Catering deleted successfully!");
					}
					else
					{
						Console.WriteLine("Catering deletion canceled.");
					}
				}
				else if (cateringIndex == 0)
				{
					break; // Exit the loop if 0 is entered
				}
				else
				{
					Console.WriteLine("\nInvalid input or catering not found.");
				}
			}
			else
			{
				Console.WriteLine("\nInvalid input. Please enter a number.");
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}
	}


	public static void ChangeCaterings()
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("Select Catering to Update:");

			var caterings = CateringDataAccess.GetCatering();
			DisplayCaterings();

			Console.WriteLine("\nEnter the number of the catering to update (0 to exit):");
			var input = Console.ReadLine();

			if (int.TryParse(input, out var cateringIndex))
			{
				if (cateringIndex >= 1 && cateringIndex <= caterings.Count)
				{
					var selectedCatering = caterings[cateringIndex - 1];

					Console.WriteLine(
						$"\nUpdating Catering: Name: {selectedCatering.Name} Description: {selectedCatering.Description} Price: {selectedCatering.Price} Is Halal: {selectedCatering.IsHalal}");

					Console.WriteLine("\nEnter the new name:");
					var newName = Console.ReadLine();
					selectedCatering.Name = newName;

					Console.WriteLine("\nEnter the new description:");
					var newDescription = Console.ReadLine();
					selectedCatering.Description = newDescription;

					Console.WriteLine("\nEnter the new price:");
					var priceInput = Console.ReadLine();
					if (decimal.TryParse(priceInput, out var newPrice))
					{
						selectedCatering.Price = newPrice;
						Console.WriteLine("Catering price updated successfully!");
					}
					else
					{
						Console.WriteLine("Invalid price input. Catering price not updated.");
					}

					Console.WriteLine("Is the catering halal? (y/n):");
					var halalInput = Console.ReadLine();
					var newIsHalal = halalInput?.ToLower() == "y";
					selectedCatering.IsHalal = newIsHalal;

					CateringDataAccess.UpdateCatering(selectedCatering);

					Console.WriteLine("\nCatering updated successfully!");
				}
				else if (cateringIndex == 0)
				{
					break;
				}
				else
				{
					Console.WriteLine("\nInvalid input or catering not found.");
				}
			}
			else
			{
				Console.WriteLine("\nInvalid input. Please enter a number.");
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}
	}


	public static void ChangeFlightsPrice(Flight flightToUpdate)
	{
		Console.Clear();
		var line = new string('-', 100);
		Console.WriteLine(line);
		Console.WriteLine(
			$"| Flight number   | Departure  | Destination  | Departure time       | Seats available  | Price     |");
		Console.WriteLine(line);
		Console.WriteLine(
			$"| {flightToUpdate.FlightNumber,-15} | {flightToUpdate.DepartureCity,-10} | {flightToUpdate.DestinationCity,-12} | {flightToUpdate.DepartureTime,-20} | {flightToUpdate.Seats.Count,-16} | {flightToUpdate.Price,-9} |");
		Console.WriteLine(line);
		Console.WriteLine();


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
		Console.Clear();
		var line = new string('-', 100);
		Console.WriteLine(line);
		Console.WriteLine(
			$"| Flight number   | Departure  | Destination  | Departure time       | Seats available  | Price     |");
		Console.WriteLine(line);
		Console.WriteLine(
			$"| {flightToUpdate.FlightNumber,-15} | {flightToUpdate.DepartureCity,-10} | {flightToUpdate.DestinationCity,-12} | {flightToUpdate.DepartureTime,-20} | {flightToUpdate.Seats.Count,-16} | {flightToUpdate.Price,-9} |");
		Console.WriteLine(line);
		Console.WriteLine();

		Console.Write("Enter the new destination city: ");
		var newDestination = Console.ReadLine();
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
		Console.Clear();
		var line = new string('-', 100);
		Console.WriteLine(line);
		Console.WriteLine(
			$"| Flight number   | Departure  | Destination  | Departure time       | Seats available  | Price     |");
		Console.WriteLine(line);
		Console.WriteLine(
			$"| {flightToUpdate.FlightNumber,-15} | {flightToUpdate.DepartureCity,-10} | {flightToUpdate.DestinationCity,-12} | {flightToUpdate.DepartureTime,-20} | {flightToUpdate.Seats.Count,-16} | {flightToUpdate.Price,-9} |");
		Console.WriteLine(line);
		Console.WriteLine();

		Console.Write("Are you sure you want to cancel this flight? (Y/N): ");
		var confirmChoice = Console.ReadKey(true);
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

	public static void DisplayAllFlights()
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("View all available flights");
			Console.WriteLine(
				$"Sort by:\n[1] Flight number\n[2] Departure city\n[3] Destination city\n[4] Departure time\n[5] Seats available\n[6] Price\n[7] Quit");
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

			Console.WriteLine("Resetting screen...");
			Console.WriteLine("\nPress any key to continue...");
			Console.ReadKey(true);
		}
	}

	public static void SortAndDisplayFlights(string sortBy, bool calledFromUpdateMenu = false)
	{
		Console.Clear();
		Console.WriteLine("Loading the list");
		var flights = BookingLogic.GetAllFlights();
		switch (sortBy)
		{
			case "FlightNumber":
				flights.Sort((f1, f2) =>
					string.Compare(f1.FlightNumber, f2.FlightNumber, StringComparison.Ordinal));
				break;

			case "DepartureCity":
				flights.Sort((f1, f2) =>
					string.Compare(f1.DepartureCity, f2.DepartureCity, StringComparison.Ordinal));
				break;

			case "DestinationCity":
				flights.Sort((f1, f2) =>
					string.Compare(f1.DestinationCity, f2.DestinationCity, StringComparison.Ordinal));
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

		var line = new string('-', 100);
		Console.WriteLine(line);
		Console.WriteLine(
			"| Flight number   | Departure  | Destination  | Departure time       | Seats available  | Price     |");
		Console.WriteLine(line);
		var startIndex = 0;
		var displayCount = 20;
		while (startIndex < flights.Count)
		{
			var endIndex = Math.Min(startIndex + displayCount, flights.Count);
			for (var i = startIndex; i < endIndex; i++)
			{
				var flight = flights[i];
				Console.WriteLine(line);
				Console.WriteLine(
					$"| {flight.FlightNumber,-14} | {flight.DepartureCity,-10} | {flight.DestinationCity,-12} | {flight.DepartureTime,-14} | {flight.Seats.Count,-15} | {flight.Price,-9} |");
			}

			Console.WriteLine(line);

			// Prompt for user input to continue displaying flights
			// Prompt for user input to navigate flights
			Console.WriteLine("Options: [B]ack, [F]orward, [R]eturn to Menu");
			Console.WriteLine(line);
			var input = Console.ReadKey(true);

			Console.Clear();

			if (input.Key == ConsoleKey.R) return;

			if (input.Key == ConsoleKey.B)
			{
				// Go back to the previous interval
				if (startIndex - displayCount >= 0) startIndex -= displayCount;
			}
			else if (input.Key == ConsoleKey.F)
			{
				// Go forward to the next interval
				if (startIndex + displayCount < flights.Count) startIndex += displayCount;
			}
		}
	}
}