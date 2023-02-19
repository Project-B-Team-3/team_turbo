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

    public void AddItem(Player player, Item item)
    {
        if (TheCountedItemList.Any(h => item == h.TheItem))
        {
            TheCountedItemList.First(h => item == h.TheItem).Quantity += 1;
        }
        else
        {
            TheCountedItemList.Add(new CountedItem(item, 1));
        }

        // Add item to player's inventory
        player.AddItem(item);
    }

    public void RemoveItem(Player player, Item item)
    {
        var itemToRemove = TheCountedItemList.FirstOrDefault(h => item == h.TheItem);

        if (itemToRemove != null)
        {
            itemToRemove.Quantity -= 1;
            if (itemToRemove.Quantity == 0)
            {
                TheCountedItemList.Remove(itemToRemove);
            }

            // Remove item from player's inventory
            player.RemoveItem(item);
        }
    }
}
