namespace Main_project.Logic;

public static class CateringLogic
{
	public static List<Catering> DefaultCateringList()
	{
		var items = new List<Catering>()
		{
			new(
				"Chicken Meal",
				"Delicious chicken meal with seasoned grilled chicken, roasted vegetables, and mashed potatoes.",
				15.99M,
				false
			),
			new(
				"Pasta Meal",
				"Classic pasta dish with your choice of marinara or alfredo sauce, served with garlic bread.",
				12.99M,
				false
			),
			new(
				"Vegetarian Pasta",
				"A vegetarian-friendly pasta dish with fresh vegetables and your choice of sauce.",
				11.99M,
				false
			),
			new(
				"Halal Chicken Meal",
				"Halal-certified chicken meal with seasoned grilled chicken, roasted vegetables, and mashed potatoes.",
				16.99M,
				true
			),
			new(
				"Halal Vegetarian Pasta",
				"A halal-certified vegetarian-friendly pasta dish with fresh vegetables and your choice of sauce.",
				12.99M,
				true
			),
			new(
				"Soft Drink",
				"Refreshing soft drink of your choice (Coke, Sprite, or Fanta).",
				2.50M,
				true
			),
			new("Bottled Water", "Bottled water for a thirst-quenching experience.", 1.99M, true)
		};

		return items;
	}
}