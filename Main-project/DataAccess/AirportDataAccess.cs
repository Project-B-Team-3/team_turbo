namespace Main_project.DataAccess;

public static class AirportDataAccess
{
    private static Airport GetAirportDetails()
    {
        return new Airport
        {
            PhoneNumber = "123-456-7890",
            Address = "123 Main Street, City, Country",
            Email = "info@example.com"
        };
    }
}
