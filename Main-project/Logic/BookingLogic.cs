using Main_project.DataAccess;
using Newtonsoft.Json;
using Main_project.DataModels;

namespace Main_project.Logic
{
    public class BookingLogic
    {
        private FlightDataAccess flightData;
        public BookingLogic()
        {
            flightData = new FlightDataAccess();
        }

        public IEnumerable<Flight> UpComingFlights(){
            var displayedFlights = flightData.GetFlights().Where(f => f.DepartureTime > DateTime.Now)
                .Where(f => f.DepartureTime < DateTime.Now + TimeSpan.FromDays(28));
            return displayedFlights;
        }

    }
}
