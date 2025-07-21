using UnityEngine;

public interface IFillsGridSlot
{
    public void Tick(GridLogic gridLogic, GridSpace gridSpace, int tick);
}
