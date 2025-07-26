using System;
using UnityEngine;

[Serializable]
public struct ItemStack
{
    // Constructors
    public ItemStack(Item item, uint quantity = 1)
    {
        if (quantity == 0)
        {
            Item = null;
        }
        else
        { 
            Item = item;
        }
        _quantity = quantity;
    } 
    
    // Fields
    public Item Item;

    [SerializeField] private uint _quantity;
    public uint Quantity
    {
        readonly get => _quantity;
        set
        {
            if (value == 0)
            {
                Item = null;
            }
            else if (value > Item.StackSize)
            {
                _quantity = Item.StackSize;
            }
            else
            {
                _quantity = value;
            }
        }
    }

    // Methods
    /// <returns>If any items were able to be added</returns>
    public static ItemStack AddItemStack(ItemStack baseStack, ItemStack addedStack, out ItemStack excessItems)
    {
        if (baseStack.Item == null)
        {
            excessItems = default;
            return addedStack;
        }
        else if (baseStack.Item != addedStack.Item || baseStack.Quantity == baseStack.Item.StackSize)
        {
            excessItems = addedStack;
            return baseStack;
        }

        uint totalItems = baseStack.Quantity + addedStack.Quantity;
        uint remainingItems = baseStack.Item.StackSize - totalItems;
        excessItems = new(baseStack.Item, remainingItems);
        return new(baseStack.Item, Math.Min(baseStack.Item.StackSize, totalItems));
    }

    public override string ToString()
    {
        string itemName = "null";
        if (Item != null) itemName = Item.name;
        return $"{itemName}*{Quantity}";
    }
}
