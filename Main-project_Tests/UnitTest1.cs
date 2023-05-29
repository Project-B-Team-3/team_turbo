using Main_project.DataAccess;
using Main_project.DataModels;
using Main_project.Misc;

namespace Main_project_Tests;

[TestClass]
public class UnitTest1
{
	public UnitTest1()
	{
		//if(!File.Exists("../Main-project/DataSources/Airplanes/Boeing737-700.json")) AirplaneDataAccess.MakePlane(new Airplane("Boeing", "737-700", 6, 106, 6, 20));
		//if(!File.Exists("../Main-project/DataSources/Airplanes/EmbraerE-175.json")) AirplaneDataAccess.MakePlane(new Airplane("Embraer", "E-175", 4, 60, 8, 20));
	}
	[TestMethod]
	public void TestFlights()
	{
		if (!File.Exists("../Main-project/DataSources/Flights.json"))
		{
			FlightGenerator.GenerateFlights();
		}
		Assert.IsTrue(FlightDataAccess.GetFlights().Count > 0);
	}
}