using Main_project.DataModels;

namespace Main_project.Logic
{
    public class CateringLogic
    {
        public static List<Catering> cateringList() // I will add more catering options later after PO interview -Menno
        {
            var items = new List<Catering>()
            {
                new Catering("Chicken", "Tasty chicken", 8.50M, true),
                new Catering("Pasta", "Tasty pasta", 10.50M, true)
            };

            return items;
        }
    }
}
