using static CardinalDirectionUtils;

public interface IContainsItemStack
{
    public ItemStack ContainedItemStack { get; set; }
    //public ItemStack TakeItem(); probably can remove, objects generally push out items instead of pulling out of other objects

    // could split off these two methods into a seperate interface called IAcceptsItems, but it doesn't feel necessary for now.
    public bool AcceptsItem(Item item, CardinalDirection direction);
}