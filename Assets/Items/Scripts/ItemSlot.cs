using System;
using UnityEngine;

/*public class ItemSlot
{
    public event Events.OnValueChanged<Item> OnItemChanged;

    public bool AcceptsAllItems;
    [HideInInspector] public Item[] AcceptedItems = new Item[0];

    private Item _item;
    public Item Item
    {
        get => _item;
        set
        {
            // make sure the item is accepted
            if (!AcceptsItem(value))
            {
                Debug.Log($"item is not accepted");
                return;
            }
            OnItemChanged?.Invoke(_item, value);
            _item = value;
        }
    }

    public Item TakeItem()
    {
        var takenItem = Item;
        Item = null;
        return takenItem;
    }

    public bool AcceptsItem(Item item)
    {
        return AcceptsAllItems || Array.IndexOf(AcceptedItems, item) != -1;
    }
}*/