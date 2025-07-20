using UnityEngine;

public interface IFillsGridSlot
{
    public void Tick(GridLogic gridLogic, Vector2Int gridPosition, int tick);
}
