using UnityEngine;
using System.Collections.Generic;

public class GridSpace : MonoBehaviour
{
    // Fields
    [SerializeField] private MachineObject machineObjectPrefab;
    [HideInInspector] public Vector2Int GridPosition;

    private IFillsGridSlot _gridObject;
    public IFillsGridSlot GridObject
    {
        get => _gridObject;
        set
        {
            // delete whatever was previously in the location
            var gridObjectScript = _gridObject as MonoBehaviour;
            if (gridObjectScript != null)
            {
                Destroy(gridObjectScript.gameObject);
            }

            _gridObject = value;
        }
    }

    public void Tick(GridLogic gridLogic, int tick)
    {
        if (GridObject != null)
        {
            GridObject.Tick(gridLogic, this, tick);
        }
    }

    public List<GridSpace> AdjacentSpaces(GridLogic gridLogic, GridSpace gridSpace)
    {
        Vector2Int[] directionOffsets = { new(0, 1), new(1, 0), new(0, -1), new(-1, 0) };

        List<GridSpace> adjacentSpaces = new();
        foreach (var direction in directionOffsets)
        {
            Vector2Int offsetPosition = gridSpace.GridPosition + direction;
            if (gridLogic.IsPositionOnGrid(offsetPosition))
            {
                adjacentSpaces.Add(gridLogic.GridSpaces[offsetPosition.x, offsetPosition.y]);
            }
        }
        return adjacentSpaces;
    }
}