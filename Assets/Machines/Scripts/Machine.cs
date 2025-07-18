using UnityEngine;
using static CardinalDirectionUtils;

public abstract class Machine : ScriptableObject
{
    // Fields
    public string MachineName;
    public Sprite MachineSprite;
    // could add a bool called ConsumesItemsToTick and an Item called ItemConsumedToTick?

    //public bool HoldsItems;
    public bool AcceptsAllItems;
    public Item[] AcceptedItems;
    public MultiCardinalDirections InputDirections;

    // Abstract Methods
    public abstract void MachineTick(GridLogic gridLogic, Vector2Int gridPosition);
    // public abstract Item CalculateOutputs(out float quantityPerSecond);
    // public abstract Item CalculateInputs(out float quantityPerSecond);

    // Methods
    // this really should be moved somewhere else
    public static bool OutputItem(GridLogic gridLogic, Vector2Int targetPosition, Item item, CardinalDirection outputDirection)
    {
        if (targetPosition.x < 0 || targetPosition.x >= gridLogic.GridSize || targetPosition.y < 0 || targetPosition.y >= gridLogic.GridSize)
        {
            Debug.Log($"targeted machine position {targetPosition} is out of bounds");
            return false;
        }

        GridSpace targetGridSpace = gridLogic.GridSpaces[targetPosition.x, targetPosition.y];

        if (targetGridSpace.GridObject != null)
        {
            IContainsItem itemContainer = targetGridSpace.GridObject as IContainsItem;
            if (itemContainer != null && itemContainer.AcceptsItem(item, outputDirection))
            {
                itemContainer.ContainedItem = item;
                return true;
            }
            else return false;
        }
        else
        {
            gridLogic.CreateDroppedItem(targetPosition, item);
            return true;
        }
    }
}