using UnityEngine;
using UnityEngine.InputSystem;

public class GridInputs : MonoBehaviour
{
    // Fields
    [SerializeField] private Hotbar hotbar;
    [SerializeField] private MachinePreview machinePreview;
    [SerializeField] private GridLogic gridLogic;
    
    // Methods
    private void Start()
    {
        ControlsManager.RotateCW.started += OnRotateCWInput;
        ControlsManager.RotateCCW.started += OnRotateCCWInput;
    }

    private void OnRotateCWInput(InputAction.CallbackContext context)
    {
        machinePreview.PreviewRotation++;
    }
    private void OnRotateCCWInput(InputAction.CallbackContext context)
    {
        machinePreview.PreviewRotation--;
    }

    public void OnGridSpaceInput(GridSpaceInputs.InputTypes inputType, Vector2Int gridSpacePosition)
    {
        var gridSpace = gridLogic.GridSpaces[gridSpacePosition.x, gridSpacePosition.y];
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
            case GridSpaceInputs.InputTypes.Hovered:
                machinePreview.OnMouseHoverMachine(gridSpace);
                break;
        }
    }
    private void GridSpaceInteractInput(GridSpace gridSpace)
    {
        // set the machine object's machine to the currently selected machine (unless no machine is selected)
        if (hotbar.CurrentSlot.MachineInSlot == null) return;
        gridLogic.CreateMachineObject(gridSpace.GridPosition, hotbar.CurrentSlot.MachineInSlot);
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
}
