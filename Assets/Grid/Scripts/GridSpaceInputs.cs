using UnityEngine;
using UnityEngine.InputSystem;

public class GridSpaceInputs : MonoBehaviour
{
    public enum InputTypes
    {
        Interact,
        Remove,
        PickItem,
        Hovered
    }
    public delegate void OnGridSpaceInput(InputTypes inputType, Vector2Int gridSpacePosition);
    public event OnGridSpaceInput GridSpaceInput;

    // Fields
    [SerializeField] private GridSpace gridSpace;
    [SerializeField] private CursorEvents cursorEvents;

    // Methods
    private void Start()
    {
        // Setup input detection
        ControlsManager.Interact.started += OnInteractInput;
        ControlsManager.Remove.started += OnRemoveInput;
        ControlsManager.PickItem.started += OnPickItemInput;
        cursorEvents.Hovered += OnHovered;
    }
    public void OnInteractInput(InputAction.CallbackContext context) => CheckInputPosition(InputTypes.Interact);
    public void OnRemoveInput(InputAction.CallbackContext context) => CheckInputPosition(InputTypes.Remove);
    public void OnPickItemInput(InputAction.CallbackContext context) => CheckInputPosition(InputTypes.PickItem);
    public void OnHovered()
    {
        GridSpaceInput?.Invoke(InputTypes.Hovered, gridSpace.GridPosition);
    }
    public void CheckInputPosition(InputTypes inputType)
    {
        if (CursorUtilities.MouseHovering(gameObject))
        {
            GridSpaceInput?.Invoke(inputType, gridSpace.GridPosition);
        }
    }
}
