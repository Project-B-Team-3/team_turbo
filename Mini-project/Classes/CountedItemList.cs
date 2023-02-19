namespace Mini_project.Classes;

public class CountedItemList
{
    public List<CountedItem> TheCountedItemList;

    public CountedItemList()
    {
        TheCountedItemList = new List<CountedItem>();
    }

    public void AddCountedItem(CountedItem item) // Adds CountedItem object to the list
    {
        TheCountedItemList.Add(item);
    }

    public void AddItem(Player player, Item item) // Checks if the item already exists in the list and updates the player's inventory accordingly
    {
        if (TheCountedItemList.Any(itemInStock => item == itemInStock.TheItem))
        {
            TheCountedItemList.First(itemInStock => item == itemInStock.TheItem).Quantity += 1;
        }
        else
        {
            TheCountedItemList.Add(new CountedItem(item, 1));
        }

        // Add item to player's inventory
        player.Inventory = new CountedItemList(TheCountedItemList);
    }

    public void RemoveItem(Player player, Item item)
    {
        var itemToRemove = TheCountedItemList.FirstOrDefault(itemInStock => item == itemInStock.TheItem);

        if (itemToRemove != null)
        {
            itemToRemove.Quantity -= 1;
            if (itemToRemove.Quantity == 0)
            {
                TheCountedItemList.Remove(itemToRemove);
            }

            // Remove item from player's inventory
            player.Inventory.RemoveItem(item);
        }
    }
}
