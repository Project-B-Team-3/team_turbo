namespace Mini_project.Classes;

public class CountedItemList
{
	public List<CountedItem> TheCountedItemList;

	public CountedItemList()
	{
		TheCountedItemList = new List<CountedItem>();
	}

	public void AddCountedItem(CountedItem item)
	{
		TheCountedItemList.Add(item);
	}

	public void AddItem(Item item)
	{
		if (TheCountedItemList.Any(h => item == h.TheItem))
		{
			TheCountedItemList.First(h => item == h.TheItem).Quantity += 1;
		}else{
			TheCountedItemList.Add(new CountedItem(item, 1));
		}
	}

	public void AddItems(CountedItemList list)
	{
		foreach (var countedItem in list.TheCountedItemList)
		{
			TheCountedItemList.Add(countedItem);
		}
	}
}