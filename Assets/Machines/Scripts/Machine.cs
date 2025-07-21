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
    public abstract void MachineTick(GridLogic gridLogic, GridSpace gridSpace, int rotation, int tick);
    // public abstract Item CalculateOutputs(out float quantityPerSecond);
    // public abstract Item CalculateInputs(out float quantityPerSecond);
}