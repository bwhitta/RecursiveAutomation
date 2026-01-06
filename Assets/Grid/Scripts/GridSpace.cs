using System.Collections.Generic;
using UnityEngine;
using static CardinalDirectionUtils;

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
        GridObject?.Tick(gridLogic, this, tick);
    }

    public List<GridSpace> GetAdjacentSpaces(GridLogic gridLogic)
    {
        Vector2Int[] directionOffsets = { new(0, 1), new(1, 0), new(0, -1), new(-1, 0) };

        List<GridSpace> adjacentSpaces = new();
        foreach (var direction in directionOffsets)
        {
            Vector2Int offsetPosition = GridPosition + direction;
            if (gridLogic.IsPositionOnGrid(offsetPosition))
            {
                adjacentSpaces.Add(gridLogic.GridSpaces[offsetPosition.x, offsetPosition.y]);
            }
        }
        return adjacentSpaces;
    }
    public List<GridSpace> MachinesTargetingSpace(GridLogic gridLogic)
    {
        List<GridSpace> machinesTargetingThis = new();
        List<GridSpace> adjacentSpaces = GetAdjacentSpaces(gridLogic);
        foreach (var adjacentSpace in adjacentSpaces)
        {
            // check if the adjacent space has a machine that targets this one
            if (adjacentSpace.GridObjectTarget(gridLogic) == this)
            {
                machinesTargetingThis.Add(adjacentSpace);
            }
        }
        Debug.Log($"{machinesTargetingThis.Count} machines are targeting this", gameObject);
        return machinesTargetingThis;
    }
    public GridSpace GridObjectTarget(GridLogic gridLogic)
    {
        var machineObject = GridObject as MachineObject;
        if (machineObject == null) return null;

        CardinalDirection outputDirection = RotateCardinalDirection(machineObject.PlacedMachine.OutputDirection, machineObject.Rotation);
        Vector2Int targetPosition = GridPosition + CardinalDirectionVector(outputDirection);
        
        if (!gridLogic.IsPositionOnGrid(targetPosition)) return null;

        return gridLogic.GridSpaces[targetPosition.x, targetPosition.y];
    }
}