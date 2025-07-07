using UnityEngine;
using UnityEngine.InputSystem;

public class GridSpaceInputs : MonoBehaviour
{
    public enum InputTypes
    {
        Interact,
        Remove,
        PickItem
    }
    public delegate void OnGridSpaceInput(InputTypes inputType, Vector2Int gridSpacePosition);
    public event OnGridSpaceInput GridSpaceInput;

    [HideInInspector] public Vector2Int GridSpacePosition;
    
    // Methods
    private void Awake()
    {
        // Setup input detection
        ControlsManager.Interact.started += OnInteractInput;
        ControlsManager.Remove.started += OnRemoveInput;
        ControlsManager.PickItem.started += OnPickItemInput;
    }
    public void OnInteractInput(InputAction.CallbackContext context) => CheckInputPosition(InputTypes.Interact);
    public void OnRemoveInput(InputAction.CallbackContext context) => CheckInputPosition(InputTypes.Interact);
    public void OnPickItemInput(InputAction.CallbackContext context) => CheckInputPosition(InputTypes.Interact);
    public void CheckInputPosition(InputTypes inputType)
    {
        if (CursorUtilities.MouseHoveringGameObject(gameObject))
        {
            GridSpaceInput?.Invoke(inputType, GridSpacePosition);
        }
    }
}
