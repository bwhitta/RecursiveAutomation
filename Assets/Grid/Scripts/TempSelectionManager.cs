using UnityEngine;
using UnityEngine.InputSystem;

// eventually will want to replace this script with something that automatically does the grouping instead
public class TempSelectionManager : MonoBehaviour
{
    // Fields
    [SerializeField] private GridLogic gridLogic;
    [SerializeField] private Vector2Int startingGridSpaceCoords;
    private MachineGroup machineGroup;

    // Methods
    private void Start()
    {
        ControlsManager.ProcessTempSelect.started += ProcessTempSelect;
    }
    private void ProcessTempSelect(InputAction.CallbackContext context)
    {
        Debug.Log($"processing temp select");
        machineGroup = new(gridLogic, gridLogic.GridSpaces[startingGridSpaceCoords.x, startingGridSpaceCoords.y]);
    }
}
