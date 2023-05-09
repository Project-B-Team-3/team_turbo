using Main_project.DataAccess;
using Main_project.Presentation;

namespace Main_project
{
    public class Program
    {
        public static void Main()
        {
            FlightDataAccess.InitFiles();
            BookingDataAccess.InitFiles();
            Menu.Start();
        }
    }
}