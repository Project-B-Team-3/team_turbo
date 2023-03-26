using Main_project.Logic;
using Main_project.DataModels;

namespace Main_project.Presentation
{
    public static class ConsoleView
    {
        public static void DisplayFlights()
        {
            if (!BookingLogic.UpComingFlights().Any())
            {
                Console.WriteLine("No upcoming flights!");
                Console.ReadKey();
            }
            else
            {
                foreach (var flight in BookingLogic.UpComingFlights())
                {
                    Console.WriteLine(flight);
                }

                Console.ReadKey();
            }
        }

        // public int GetFlightSelection()
        // {
        //     int selection = 0;
        //     do
        //     {
        //         Console.Write("Enter the number of the flight you want to book: ");
        //     } while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > flights.Count);
        //     return selection;
        // }
    }
}
