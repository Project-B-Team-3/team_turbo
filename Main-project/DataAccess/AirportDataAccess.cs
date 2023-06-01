using Main_project.DataModels;

namespace Main_project.DataAccess
{
    public static class AirportDataAccess
    {
        public static Airport GetAirportDetails()
        {
            return new Airport
            {
                PhoneNumber = "010-6942069",
                Address = "Driemanssteeweg 107, 3011 WN in Rotterdam",
                Email = "info@darcyairlines.com"
            };
        }
    }
}