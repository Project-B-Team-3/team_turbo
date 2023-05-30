using Main_project.DataModels;

namespace Main_project.DataAccess
{
    public static class CateringDataAccess
    {
        public static void InitFiles()
        {
            if (File.Exists("./DataSources/Catering.json")) return;
            File.WriteAllText("./DataSources/Catering.json", "[]");
        }

        public static List<Catering> GetCatering()
        {
            try
            {
                var json = File.ReadAllText("./DataSources/Catering.json");
                var cateringItems = JsonConvert.DeserializeObject<List<Catering>>(json);
                return cateringItems ?? new List<Flight>();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e);
                Environment.Exit(1);
                return new List<Catering>();
            }
        }

        public static void CreateCateringItems(List<Catering> cateringList)
        {
            var newCatering = GetCatering();
            newCatering = newCatering.Concat(cateringList).ToList();
            File.WriteAllText("./DataSources/Flights.json", JsonConvert.SerializeObject(newCatering, Formatting.Indented));
        }
    }
}