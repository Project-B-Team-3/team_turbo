using Main_project.DataModels;
using System.IO;
using Newtonsoft.Json;

namespace Main_project.DataAccess;

public static class CateringDataAccess
{
	public static List<Catering> GetCatering()
	{
		var path = "./DataSources/Catering.json";
		if (!File.Exists(path)) File.Create(path).Close();
		
		var json = File.ReadAllText(path);
		var cateringItems = JsonConvert.DeserializeObject<List<Catering>>(json);
		return cateringItems ?? new List<Catering>();
	}

	public static void CreateCateringItems(List<Catering> cateringList)
	{
		var newCatering = GetCatering();
		newCatering = newCatering.Concat(cateringList).ToList();
		File.WriteAllText("./DataSources/Catering.json", JsonConvert.SerializeObject(newCatering, Formatting.Indented));
	}
}