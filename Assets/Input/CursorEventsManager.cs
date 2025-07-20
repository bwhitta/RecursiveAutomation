using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CursorEventsManager : MonoBehaviour
{
    public static CursorEventsManager Instance;
    public delegate void OnHoverEnd();
    public event OnHoverEnd HoveringEnded;

    [HideInInspector] public List<CursorEvents> CursorEvents = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        ControlsManager.Point.performed += OnPointerMoved;
        ControlsManager.Point.canceled += OnPointerCancelled;
    }


    private void OnPointerMoved(InputAction.CallbackContext context)
    {
        CursorEvents hovered = CursorUtilities.MouseHovering(CursorEvents);
        if (hovered != null)
        {
            hovered.Hovered?.Invoke();
        }
        else
        {
            HoveringEnded?.Invoke();
        }
    }
    // Generally this is called when the player tabs out or clicks off the game
    private void OnPointerCancelled(InputAction.CallbackContext context)
    {
        HoveringEnded?.Invoke();
    }
}