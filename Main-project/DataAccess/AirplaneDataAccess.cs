using Main_project.DataModels;
using Newtonsoft.Json;

namespace Main_project.DataAccess;

public static class AirplaneDataAccess
{
	public static List<Airplane> GetPlanes()
	{
		return Directory.EnumerateFiles("./DataSources/Airplanes")
			.Select(file => JsonConvert.DeserializeObject<Airplane>(File.ReadAllText(file)))
			.Where(plane => plane != null).ToList();
	}

	public static bool MakePlane(Airplane plane)
	{
		File.WriteAllText("./DataSources/Airplanes/" + plane.Brand + plane.Model + ".json",
			JsonConvert.SerializeObject(plane, Formatting.Indented));
		return true;
	}
}