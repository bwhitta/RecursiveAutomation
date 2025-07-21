using UnityEngine;
using static CardinalDirectionUtils;

public static class ItemManagement
{
    public static bool OutputItems(GridLogic grid, Vector2Int targetPosition, ItemStack producedItems, CardinalDirection outputDirection, out ItemStack excessItems)
    {
        GridSpace targetGridSpace = grid.GridSpaces[targetPosition.x, targetPosition.y];
        if (targetGridSpace.GridObject != null)
        {
            var targetInventory = targetGridSpace.GridObject as IContainsItemStack;
            if (targetInventory != null && targetInventory.AcceptsItem(producedItems.Item, outputDirection))
            {
                targetInventory.ContainedItemStack = ItemStack.AddItemStack(targetInventory.ContainedItemStack, producedItems, out excessItems);
                bool succesfulOutput = excessItems.Quantity != producedItems.Quantity;
                return succesfulOutput;
            }
            else
            {
                excessItems = producedItems;
                return false;
            }
        }
        else
        {

            grid.CreateDroppedItem(targetPosition, producedItems);
            excessItems = default;
            return true;
        }
    }
}
