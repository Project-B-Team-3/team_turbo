using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Presentation;

namespace Main_project
{
    public class Program
    {
        public static void Main()
        {
            if(!File.Exists("./DataSources/Airplanes/Boeing737-700.json")) AirplaneDataAccess.MakePlane(new Airplane("Boeing", "737-700", 6, 106, 6, 20));
            if(!File.Exists("./DataSources/Airplanes/EmbraerE-175.json")) AirplaneDataAccess.MakePlane(new Airplane("Embraer", "E-175", 4, 60, 8, 20));
            FlightDataAccess.InitFiles();
            BookingDataAccess.InitFiles();
            Menu.Start();
        }
    }
}