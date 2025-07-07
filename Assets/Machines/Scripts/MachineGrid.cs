using UnityEngine;
using UnityEngine.EventSystems;

public class MachineGrid : MonoBehaviour
{
    // Fields
    [SerializeField] private Hotbar hotbar;
    [HideInInspector] public MachineObject[,] Machines;

    // Methods
    public void OnGridSpaceClicked(GridSpaceInputs.InputTypes inputType, Vector2Int gridSpacePosition)
    {
        var machineObject = Machines[gridSpacePosition.x, gridSpacePosition.y];
        switch (inputType)
        {
            case GridSpaceInputs.InputTypes.Interact:
                GridSpaceInteractInput(machineObject);
                break;
            case GridSpaceInputs.InputTypes.Remove:
                GridSpaceRemoveInput(machineObject);
                break;
            case GridSpaceInputs.InputTypes.PickItem:
                GridSpacePickItemInput(machineObject);
                break;
        }
    }

    private void GridSpaceInteractInput(MachineObject machineObject)
    {
        // set the machine object's machine to the currently selected machine (unless no machine is selected)
        if (hotbar.CurrentSlot.MachineInSlot == null) return;
        machineObject.PlacedMachine = hotbar.CurrentSlot.MachineInSlot;
    }
    private void GridSpaceRemoveInput(MachineObject machineObject)
    {
        // remove the placed machine
        machineObject.PlacedMachine = null;
    }
    private void GridSpacePickItemInput(MachineObject machineObject)
    {
        // do nothing if the machine's MachineObject is null
        if (machineObject.PlacedMachine == null) return;

        for (int i = 0; i < hotbar.Slots.Length; i++)
        {
            // find if the hotbar has a machine matching the MachineObjects
            if (machineObject.PlacedMachine == hotbar.Slots[i].MachineInSlot)
            {
                // if so, change the selected slot to that index
                hotbar.SelectedSlot = i;
                break;
            }
        }
    }
}
