namespace Mini_project.Classes;

public class CountedItemList
{
    public List<CountedItem> TheCountedItemList;

    public CountedItemList()
    {
        this.TheCountedItemList = new List<CountedItem>();
    }

    public void AddCountedItem(CountedItem item)
    {
        this.TheCountedItemList.Add(item);
    }

    //TODO Make it so that it adds 1 to the counter when an item is already in the list.
    public void AddItem(Item item)
    {
        this.TheCountedItemList.Add(new CountedItem(item, 1));
    }
}