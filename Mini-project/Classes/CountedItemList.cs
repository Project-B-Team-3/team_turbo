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

	//TODO Make it so that it adds 1 to the counter when an item is already in the list.
	public void AddItem(Item item)
	{
		TheCountedItemList.Add(new CountedItem(item, 1));
	}
}