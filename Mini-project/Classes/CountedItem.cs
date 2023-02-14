namespace Mini_project.Classes;

public class CountedItem {

  public Item TheItem;
  public int Quantity;

  public CountedItem(Item item, int count)
  {
      this.TheItem = item;
      this.Quantity = count;
  }

}