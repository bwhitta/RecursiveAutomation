using UnityEngine;
using System.Collections.Generic;
using static CardinalDirectionUtils;

public abstract class Machine : ScriptableObject
{
    // Fields
    public string MachineName;
    public Sprite MachineSprite;

    public Recipe[] Recipes;
    public MultiCardinalDirections InputDirections;
    public CardinalDirection OutputDirection;

    // Abstract Methods
    public abstract void MachineTick(GridLogic gridLogic, GridSpace gridSpace, int rotation, int tick);
    public abstract bool AcceptsItem(Item item);
}