using System;
using System.Collections;
using System.Collections.Generic;
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
            _quantity = value;
        }
    }

    // Methods
    public static ItemStack AddItemStacks(ItemStack baseStack, ItemStack addedStack, out ItemStack excessItems, bool limitStackSize = false)
    {
        if (baseStack.Item == null)
        {
            excessItems = default;
            return addedStack;
        }
        else if (baseStack.Item != addedStack.Item)
        {
            // Item stacks not compatible
            excessItems = addedStack;
            return baseStack;
        }
        else
        {
            uint totalItems = baseStack.Quantity + addedStack.Quantity;

            // Calculate excess items
            if (limitStackSize)
            {
                excessItems = new(baseStack.Item, baseStack.Item.StackSize - totalItems);
                return new(baseStack.Item, Math.Min(baseStack.Item.StackSize, totalItems));
            }
            else
            {
                excessItems = default;
                return new(baseStack.Item, totalItems);
            }
        }
    }

    public override readonly string ToString()
    {
        string itemName = "null";
        if (Item != null) itemName = Item.name;
        return $"{itemName}*{Quantity}";
    }

}