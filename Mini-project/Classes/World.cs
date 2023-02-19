namespace Mini_project.Classes;

public static class World
{
	public static readonly List<Item> Items = new();
	public static readonly List<Weapon> Weapons = new();
	public static readonly List<Monster> Monsters = new();
	public static readonly List<Quest> Quests = new();
	public static readonly List<Location> Locations = new();
	public static readonly Random RandomGenerator = new();

	public const int WEAPON_ID_RUSTY_SWORD = 1;
	public const int WEAPON_ID_CLUB = 2;

	public const int ITEM_ID_RAT_TAIL = 1;
	public const int ITEM_ID_PIECE_OF_FUR = 2;
	public const int ITEM_ID_SNAKE_FANG = 3;
	public const int ITEM_ID_SNAKESKIN = 4;
	public const int ITEM_ID_SPIDER_FANG = 5;
	public const int ITEM_ID_SPIDER_SILK = 6;
	public const int ITEM_ID_ADVENTURER_PASS = 7;
	public const int ITEM_ID_WINNERS_MEDAL = 8;

	public const int MONSTER_ID_RAT = 1;
	public const int MONSTER_ID_SNAKE = 2;
	public const int MONSTER_ID_GIANT_SPIDER = 3;

	public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1;
	public const int QUEST_ID_CLEAR_FARMERS_FIELD = 2;
	public const int QUEST_ID_COLLECT_SPIDER_SILK = 3;

	public const int LOCATION_ID_HOME = 1;
	public const int LOCATION_ID_TOWN_SQUARE = 2;
	public const int LOCATION_ID_GUARD_POST = 3;
	public const int LOCATION_ID_ALCHEMIST_HUT = 4;
	public const int LOCATION_ID_ALCHEMISTS_GARDEN = 5;
	public const int LOCATION_ID_FARMHOUSE = 6;
	public const int LOCATION_ID_FARM_FIELD = 7;
	public const int LOCATION_ID_BRIDGE = 8;
	public const int LOCATION_ID_SPIDER_FIELD = 9;

	static World()
	{
		PopulateItems();
		PopulateWeapons();
		PopulateMonsters();
		PopulateQuests();
		PopulateLocations();
	}

	public static void PopulateItems()
	{
		Items.Add(new Item(ITEM_ID_RAT_TAIL, "Rat tail", "Rat tails"));
		Items.Add(new Item(ITEM_ID_PIECE_OF_FUR, "Piece of fur", "Pieces of fur"));
		Items.Add(new Item(ITEM_ID_SNAKE_FANG, "Snake fang", "Snake fangs"));
		Items.Add(new Item(ITEM_ID_SNAKESKIN, "Snakeskin", "Snakeskins"));
		Items.Add(new Item(ITEM_ID_SPIDER_FANG, "Spider fang", "Spider fangs"));
		Items.Add(new Item(ITEM_ID_SPIDER_SILK, "Spider silk", "Spider silks"));
		Items.Add(new Item(ITEM_ID_ADVENTURER_PASS, "Adventurer pass", "Adventurer passes"));
		Items.Add(new Item(ITEM_ID_WINNERS_MEDAL, "Winner's medal", "winner's medals"));
	}

	public static void PopulateWeapons()
	{
		Weapons.Add(new Weapon(WEAPON_ID_RUSTY_SWORD, "Rusty sword", "Rusty swords", 0, 5));
		Weapons.Add(new Weapon(WEAPON_ID_CLUB, "Club", "Clubs", 3, 10));
	}

	public static void PopulateMonsters()
	{
		var rat = new Monster(MONSTER_ID_RAT, "rat", "rats", 5, 3, 10, 3, 3);
		rat.Loot.AddItem(player, ItemByID(ITEM_ID_RAT_TAIL));
		rat.Loot.AddItem(player, ItemByID(ITEM_ID_PIECE_OF_FUR));

		var snake = new Monster(MONSTER_ID_SNAKE, "snake", "snakes", 5, 4, 20, 7, 7);
		snake.Loot.AddItem(player, ItemByID(ITEM_ID_SNAKE_FANG));
		snake.Loot.AddItem(player, ItemByID(ITEM_ID_SNAKESKIN));

		var giantSpider = new Monster(MONSTER_ID_GIANT_SPIDER, "giant spider", "giant spiders", 5, 5, 30, 10, 10);
		giantSpider.Loot.AddItem(player, ItemByID(ITEM_ID_SPIDER_FANG));
		giantSpider.Loot.AddItem(player, ItemByID(ITEM_ID_SPIDER_SILK));

		Monsters.Add(rat);
		Monsters.Add(snake);
		Monsters.Add(giantSpider);
	}

	public static void PopulateQuests()
	{
		var clearAlchemistGarden =
			new Quest(
				QUEST_ID_CLEAR_ALCHEMIST_GARDEN,
				"Clear the alchemist's garden",
				"Kill rats in the alchemist's garden ", 20, 10,
				null,
				WeaponByID(WEAPON_ID_CLUB));

		clearAlchemistGarden.QuestCompletionItems.AddCountedItem(new CountedItem(ItemByID(ITEM_ID_RAT_TAIL), 3));

		var clearFarmersField =
			new Quest(
				QUEST_ID_CLEAR_FARMERS_FIELD,
				"Clear the farmer's field",
				"Kill snakes in the farmer's field", 20, 20,
				ItemByID(ITEM_ID_ADVENTURER_PASS),
				null);

		clearFarmersField.QuestCompletionItems.AddCountedItem(new CountedItem(ItemByID(ITEM_ID_SNAKE_FANG), 3));

		var clearSpidersForest =
			new Quest(
				QUEST_ID_COLLECT_SPIDER_SILK,
				"Collect spider silk",
				"Kill spiders in the spider forest", 20, 30,
				ItemByID(ITEM_ID_WINNERS_MEDAL),
				null);

		clearSpidersForest.QuestCompletionItems.AddCountedItem(new CountedItem(ItemByID(ITEM_ID_SPIDER_SILK), 3));

		Quests.Add(clearAlchemistGarden);
		Quests.Add(clearFarmersField);
		Quests.Add(clearSpidersForest);
	}

	public static void PopulateLocations()
	{
		// Create each location
		var home = new Location(LOCATION_ID_HOME, "Home", "Your house. You really need to clean up the place.", null,
			null, null);

		var townSquare = new Location(LOCATION_ID_TOWN_SQUARE, "Town square", "You see a fountain.", null, null, null);

		var alchemistHut = new Location(LOCATION_ID_ALCHEMIST_HUT, "Alchemist's hut",
			"There are many strange plants on the shelves.", null, null, null);
		alchemistHut.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN);

		var alchemistsGarden = new Location(LOCATION_ID_ALCHEMISTS_GARDEN, "Alchemist's garden",
			"Many plants are growing here.", null, null, null);
		alchemistsGarden.MonsterLivingHere = MonsterByID(MONSTER_ID_RAT);

		var farmhouse = new Location(LOCATION_ID_FARMHOUSE, "Farmhouse",
			"There is a small farmhouse, with a farmer in front.", null, null, null);
		farmhouse.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD);

		var farmersField = new Location(LOCATION_ID_FARM_FIELD, "Farmer's field",
			"You see rows of vegetables growing here.", null, null, null);
		farmersField.MonsterLivingHere = MonsterByID(MONSTER_ID_SNAKE);

		var guardPost = new Location(LOCATION_ID_GUARD_POST, "Guard post",
			"There is a large, tough-looking guard here.", ItemByID(ITEM_ID_ADVENTURER_PASS), null, null);

		var bridge = new Location(LOCATION_ID_BRIDGE, "Bridge", "A stone bridge crosses a wide river.", null, null,
			null);
		bridge.QuestAvailableHere = QuestByID(QUEST_ID_COLLECT_SPIDER_SILK);

		var spiderField = new Location(LOCATION_ID_SPIDER_FIELD, "Forest",
			"You see spider webs covering covering the trees in this forest.", null, null, null);
		spiderField.MonsterLivingHere = MonsterByID(MONSTER_ID_GIANT_SPIDER);

		// Link the locations together
		home.LocationToNorth = townSquare;

		townSquare.LocationToNorth = alchemistHut;
		townSquare.LocationToSouth = home;
		townSquare.LocationToEast = guardPost;
		townSquare.LocationToWest = farmhouse;

		farmhouse.LocationToEast = townSquare;
		farmhouse.LocationToWest = farmersField;

		farmersField.LocationToEast = farmhouse;

		alchemistHut.LocationToSouth = townSquare;
		alchemistHut.LocationToNorth = alchemistsGarden;

		alchemistsGarden.LocationToSouth = alchemistHut;

		guardPost.LocationToEast = bridge;
		guardPost.LocationToWest = townSquare;

		bridge.LocationToWest = guardPost;
		bridge.LocationToEast = spiderField;

		spiderField.LocationToWest = bridge;

		// Add the locations to the static list
		Locations.Add(home);
		Locations.Add(townSquare);
		Locations.Add(guardPost);
		Locations.Add(alchemistHut);
		Locations.Add(alchemistsGarden);
		Locations.Add(farmhouse);
		Locations.Add(farmersField);
		Locations.Add(bridge);
		Locations.Add(spiderField);
	}

	public static Location LocationByID(int id)
	{
		foreach (var location in Locations)
			if (location.Id == id)
				return location;

		return null;
	}

	public static Weapon WeaponByID(int id)
	{
		foreach (var item in Weapons)
			if (item.Id == id)
				return item;

		return null;
	}

	public static Item ItemByID(int id)
	{
		foreach (var item in Items)
			if (item.Id == id)
				return item;

		return null;
	}

	public static Monster MonsterByID(int id)
	{
		foreach (var monster in Monsters)
			if (monster.Id == id)
				return monster;

		return null;
	}

	public static Quest QuestByID(int id)
	{
		foreach (var quest in Quests)
			if (quest.Id == id)
				return quest;

		return null;
	}
}
