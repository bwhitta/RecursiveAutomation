using UnityEngine;

public interface IFillsGridSlot
{
    // could make this inherent ITickable, but some things like items don't have any need to be ticked
    public void Tick(GridLogic gridLogic, Vector2Int gridPosition);
    // put anything needed of things that fit grid slots here
}
