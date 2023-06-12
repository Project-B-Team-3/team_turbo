using Main_project.Logic;
using Newtonsoft.Json;

public static class CateringDataAccess
{
    private static readonly string cateringFilePath = "./DataSources/Catering.json";

    public static List<Catering> GetCatering()
    {
        if (!File.Exists(cateringFilePath))
        {
            var defaultCatering = CateringLogic.DefaultCateringList();
            File.WriteAllText(cateringFilePath, JsonConvert.SerializeObject(defaultCatering, Formatting.Indented));
        }

        var json = File.ReadAllText(cateringFilePath);
        var cateringItems = JsonConvert.DeserializeObject<List<Catering>>(json);
        return cateringItems ?? new List<Catering>();
    }

    public static void CreateCateringItem(Catering catering)
    {
        var cateringItems = GetCatering();
        cateringItems.Add(catering);
        SaveCateringItems(cateringItems);
    }

    public static void DeleteCatering(Catering catering)
    {
        var cateringItems = GetCatering();
        cateringItems.Remove(catering);
        SaveCateringItems(cateringItems);
    }

    public static void UpdateCatering(Catering updatedCatering)
    {
        var cateringItems = GetCatering();
        var cateringToUpdate = cateringItems.FirstOrDefault(c => c.Name == updatedCatering.Name);

        if (cateringToUpdate != null)
        {
            cateringToUpdate.Description = updatedCatering.Description;
            cateringToUpdate.Price = updatedCatering.Price;
            SaveCateringItems(cateringItems);
        }
    }

    private static void SaveCateringItems(List<Catering> cateringItems)
    {
        var json = JsonConvert.SerializeObject(cateringItems, Formatting.Indented);
        File.WriteAllText(cateringFilePath, json);
    }
}
