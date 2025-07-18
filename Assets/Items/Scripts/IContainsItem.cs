using static CardinalDirectionUtils;

public interface IContainsItem
{
    public Item ContainedItem { get; set; }
    public Item TakeItem();

    // could split off these two methods into a seperate interface called IAcceptsItems, but it doesn't feel necessary for now.
    public bool AcceptsItem(Item item, CardinalDirection direction);
    //public void InsertItem(Item item); should be easier to just directly set this, if they need something to trigger when the item is set then you can use the property
}