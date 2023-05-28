using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Presentation;

namespace Main_project
{
    public class Program
    {
        public static void Main()
        {
            if(!File.Exists("./DataSources/Airplanes/Boeing737.json")) AirplaneDataAccess.MakePlane(new Airplane("Boeing", "737", 147, 6, 0));
            FlightDataAccess.InitFiles();
            BookingDataAccess.InitFiles();
            Menu.Start();
        }
    }
}