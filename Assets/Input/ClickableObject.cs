using UnityEngine;
using UnityEngine.InputSystem;

public class ClickableObject : MonoBehaviour
{
    // Events
    public delegate void OnObjectClicked(ClickableObject interactedObject);
    public event OnObjectClicked ObjectClicked;

    // Methods
    private void Awake()
    {
        ControlsManager.Interact.started += OnClick;
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (CursorUtilities.MouseHovering(gameObject))
        {
            ObjectClicked?.Invoke(this);
        }
    }
}
