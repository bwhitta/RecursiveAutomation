using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickableObject : MonoBehaviour
{
    // Events
    public event Action<ClickableObject> ObjectClicked;

    // Methods
    private void Awake()
    {
        ControlsManager.Interact.started += OnClick;
    }

    // could change this to check via the CursorEventsManager, and this script just registers itself to that
    public void OnClick(InputAction.CallbackContext context)
    {
        if (CursorUtilities.MouseHovering(gameObject))
        {
            ObjectClicked?.Invoke(this);
        }
    }
}
