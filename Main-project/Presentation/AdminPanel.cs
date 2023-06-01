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
			                  "[2] Change seat pricing\n" +
			                  "[3] Update catering\n" +
			                  "[4] Change or cancel flights\n" +
			                  "[5] Quit");
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
					Console.WriteLine("Update catering");
					SelectCateringToUpdate();
					break;

				case ConsoleKey.D4:
					Console.WriteLine("Change or cancel flights");
					SelectFlightToUpdate();
					break;

				case ConsoleKey.D5:
					Console.WriteLine("Exiting the program");
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

			Console.WriteLine("Resetting screen...");
			Console.WriteLine("\nPress any key to continue...");
			Console.Clear();
			Console.ReadKey(true);
		}
	}

	public static void SelectFlightToUpdate()
	{
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("Flight Update Panel\n");
		Console.ForegroundColor = ConsoleColor.White;
		Console.Write("Enter the flight number to update: ");
		var flightNumber = Console.ReadLine();

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

		var flightToUpdate = BookingLogic.GetFlightByNumber(flightNumber);

		Console.WriteLine(
			$"Selected flight: {flightToUpdate.FlightNumber} - {flightToUpdate.DepartureCity} to {flightToUpdate.DestinationCity}");

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

			Console.WriteLine("The list of available caterings is:");

			var caterings = CateringDataAccess.GetCatering();
			DisplayCaterings(caterings);


			Console.WriteLine("\nSelect an option:\n" +
			                  "[1] Add a new catering item\n" +
			                  "[2] Change a catering item\n" +
			                  "[3] Delete a catering item\n" +
			                  "[4] Return to Admin");


			var input = Console.ReadKey(true);

			switch (input.Key)
			{
				case ConsoleKey.D1:
					Console.WriteLine("Add a new catering item");
					;
					AddCatering();
					break;

				case ConsoleKey.D2:
					Console.WriteLine("Change a catering item");
					;
					ChangeCaterings();
					break;

				case ConsoleKey.D3:
					Console.WriteLine("Delete a catering item");
					;
					DeleteCatering();
					break;

				case ConsoleKey.D4:
					Console.WriteLine("return to Admin.");
					;
					break;

				default:
					Console.WriteLine("Invalid option.");
					break;
			}
		}
	}

	public static void DisplayCaterings(List<Catering> caterings)
	{
		var line = new string('-', 100);
		Console.WriteLine(line);
		Console.WriteLine("| Name             | Description           | Price     | Is Halal |");
		Console.WriteLine(line);

		foreach (var catering in caterings)
		{
			Console.WriteLine(
				$"{catering.Name,-18} | {catering.Description,-22} | {catering.Price,-9} | {catering.IsHalal,-9} |");
			Console.WriteLine(line);
		}
	}

	public static void AddCatering()
	{
		Console.Clear();
		Console.WriteLine("Add New Catering Option:");

		var caterings = CateringDataAccess.GetCatering();
		DisplayCaterings(caterings);

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
		Console.Clear();
		Console.WriteLine("Select Catering to Delete:");

		var caterings = CateringDataAccess.GetCatering();
		DisplayCaterings(caterings);


		Console.WriteLine("\nEnter the number of the catering to delete:");
		var input = Console.ReadLine();

		if (int.TryParse(input, out var cateringIndex) && cateringIndex >= 1 && cateringIndex <= caterings.Count)
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
		else
		{
			Console.WriteLine("\nInvalid input or catering not found.");
		}

		Console.WriteLine("Press any key to go back...");
		Console.ReadKey();
	}

	public static void ChangeCaterings()
	{
		Console.Clear();
		Console.WriteLine("Select Catering to Update:");

		var caterings = CateringDataAccess.GetCatering();
		DisplayCaterings(caterings);

		Console.WriteLine("\nEnter the number of the catering to update:");
		var input = Console.ReadLine();

		if (int.TryParse(input, out var cateringIndex) && cateringIndex >= 1 && cateringIndex <= caterings.Count)
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
		else
		{
			Console.WriteLine("\nInvalid input or catering not found.");
		}

		Console.WriteLine("Press any key to go back...");
		Console.ReadKey();
	}


	public static void ChangeFlightsPrice(Flight flightToUpdate)
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
		Console.WriteLine(
			$"Selected flight: {flightToUpdate.FlightNumber} - {flightToUpdate.DepartureCity} to {flightToUpdate.DestinationCity}");

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

	public static void SortAndDisplayFlights(string sortBy)
	{
		Console.Clear();
		var flights = BookingLogic.GetAllFlights();
		switch (sortBy)
		{
			case "FlightNumber":
				flights.Sort((f1, f2) => string.Compare(f1.FlightNumber, f2.FlightNumber, StringComparison.Ordinal));
				break;

			case "DepartureCity":
				flights.Sort((f1, f2) => string.Compare(f1.DepartureCity, f2.DepartureCity, StringComparison.Ordinal));
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
		var displayCount = 25;
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
			Console.WriteLine("Options: [B]ack, [F]orward, [R]eturn to Sort Menu");
			Console.WriteLine(line);
			var input = Console.ReadKey(true);

			Console.Clear();

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
			else if (input.Key == ConsoleKey.R)
			{
				// Return to the sort menu
				return;
			}
		}
	}
}