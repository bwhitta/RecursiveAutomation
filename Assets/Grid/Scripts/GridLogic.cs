using UnityEngine;

public class GridLogic : MonoBehaviour
{
    // Fields
    public int GridSize;
    [SerializeField] private Hotbar hotbar;
    [HideInInspector] public GridSpace[,] GridSpaces;

    [SerializeField] private MachineObject machineObjectPrefab;
    [SerializeField] private DroppedItem droppedItemPrefab;

    // Methods
    public void OnGridSpaceInput(GridSpaceInputs.InputTypes inputType, Vector2Int gridSpacePosition)
    {
        var gridSpace = GridSpaces[gridSpacePosition.x, gridSpacePosition.y];
        switch (inputType)
        {
            case GridSpaceInputs.InputTypes.Interact:
                GridSpaceInteractInput(gridSpace);
                break;
            case GridSpaceInputs.InputTypes.Remove:
                GridSpaceRemoveInput(gridSpace);
                break;
            case GridSpaceInputs.InputTypes.PickItem:
                GridSpacePickItemInput(gridSpace);
                break;
        }
    }

    private void GridSpaceInteractInput(GridSpace gridSpace)
    {
        // set the machine object's machine to the currently selected machine (unless no machine is selected)
        if (hotbar.CurrentSlot.MachineInSlot == null) return;
        CreateMachineObject(gridSpace.GridPosition, hotbar.CurrentSlot.MachineInSlot);
    }
    private void GridSpaceRemoveInput(GridSpace gridSpace)
    {
        // remove the placed machine
        gridSpace.GridObject = null;
    }
    private void GridSpacePickItemInput(GridSpace gridSpace)
    {
        var machineObject = gridSpace.GridObject as MachineObject;
        if (machineObject == null) return;

        for (int i = 0; i < hotbar.Slots.Length; i++)
        {
            // find if the hotbar has a machine matching the MachineObjects
            if (machineObject == hotbar.Slots[i].MachineInSlot)
            {
                // if so, change the selected slot to that index
                hotbar.SelectedSlot = i;
                break;
            }
        }
    }

    private void CreateMachineObject(Vector2Int gridPosition, Machine machine)
    {
        MachineObject machineObject = Instantiate(machineObjectPrefab, GridSpaces[gridPosition.x, gridPosition.y].transform);
        machineObject.PlacedMachine = machine;
        GridSpaces[gridPosition.x, gridPosition.y].GridObject = machineObject;
    }
    public void CreateDroppedItem(Vector2Int gridPosition, Item item)
    {
        DroppedItem droppedItem = Instantiate(droppedItemPrefab, GridSpaces[gridPosition.x, gridPosition.y].transform);
        droppedItem.ContainedItem = item;
        GridSpaces[gridPosition.x, gridPosition.y].GridObject = droppedItem;
    }
}
