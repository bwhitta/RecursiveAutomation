using UnityEngine;
using static CardinalDirectionUtils;

public static class ItemManagement
{
    public static bool OutputItem(GridLogic grid, Vector2Int targetPosition, Item item, CardinalDirection outputDirection)
    {
        GridSpace targetGridSpace = grid.GridSpaces[targetPosition.x, targetPosition.y];
        if (targetGridSpace.GridObject != null)
        {
            var targetInventory = targetGridSpace.GridObject as IContainsItem;
            if (targetInventory != null)
            {
                return InsertItem(item, targetInventory, outputDirection);
            }
            else
            {
                return false;
            }
        }
        else
        {
            grid.CreateDroppedItem(targetPosition, item);
            return true;
        }
    }

    public static bool InsertItem(Item item, IContainsItem targetInventory, CardinalDirection direction)
    {
        if (targetInventory.AcceptsItem(item, direction))
        {
            targetInventory.ContainedItem = item;
            return true;
        }
        else return false;
    }
}
