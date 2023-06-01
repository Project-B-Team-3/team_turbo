using Main_project.DataModels;

namespace Main_project.DataAccess
{
    public class AirportDataAccess
    {
        public Airport GetAirportDetails()
        {
            return new Airport("010-6942069", "Driemanssteeweg 107, 3011 WN in Rotterdam", "info@darcyairlines.com");
        }
    }
}