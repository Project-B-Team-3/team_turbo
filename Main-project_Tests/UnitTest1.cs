using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Misc;

namespace Main_project_Tests;

[TestClass]
public class UnitTest1
{
	public UnitTest1()
	{
		Directory.CreateDirectory("./DataSources/Airplanes");
		if(!File.Exists("./DataSources/Airplanes/Boeing737-700.json"))
		{
			AirplaneDataAccess.MakePlane(new Airplane("Boeing", "737-700", 6, 106, 6, 20));
		}
		if (!File.Exists("./DataSources/Flights.json"))
		{
			FlightGenerator.GenerateFlights();
		}
	}
	
	[TestMethod]
	public void TestFlights()
	{
		Assert.IsTrue(FlightDataAccess.GetFlights().Count > 0);
	}
}