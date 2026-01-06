using UnityEngine;
using static CardinalDirectionUtils;

public abstract class Machine : ScriptableObject
{
    // Fields
    public string MachineName;
    public Sprite MachineSprite;

    public Recipe MachineRecipe;
    public MultiCardinalDirections InputDirections;
    public CardinalDirection OutputDirection;

    // Abstract Methods
    public abstract void MachineTick(GridLogic gridLogic, GridSpace gridSpace, int rotation, int tick);
    public abstract bool AcceptsItem(Item item);
}