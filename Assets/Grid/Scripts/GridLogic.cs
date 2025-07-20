using UnityEngine;

// Core functionality of the grid. Can rename if I come up with something better.
public class GridLogic : MonoBehaviour
{
    // Fields
    [SerializeField] private MachineObject machineObjectPrefab;
    [SerializeField] private DroppedItem droppedItemPrefab;
    [SerializeField] private MachinePreview machinePreview;
    public int GridSize;
    [HideInInspector] public GridSpace[,] GridSpaces;

    // Methods
    public void CreateMachineObject(Vector2Int gridPosition, Machine machine)
    {
        MachineObject machineObject = Instantiate(machineObjectPrefab, GridSpaces[gridPosition.x, gridPosition.y].transform);
        machineObject.Rotation = machinePreview.PreviewRotation;
        machineObject.PlacedMachine = machine;
        GridSpaces[gridPosition.x, gridPosition.y].GridObject = machineObject;
    }
    public void CreateDroppedItem(Vector2Int gridPosition, Item item)
    {
        DroppedItem droppedItem = Instantiate(droppedItemPrefab, GridSpaces[gridPosition.x, gridPosition.y].transform);
        droppedItem.ContainedItem = item;
        GridSpaces[gridPosition.x, gridPosition.y].GridObject = droppedItem;
    }
    public bool IsPositionOnGrid(Vector2Int position)
    {
        return position.x >= 0 && position.x < GridSize && position.y >= 0 && position.y < GridSize;
    }
}
