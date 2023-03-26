using Main_project.Logic;

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
    }
}
