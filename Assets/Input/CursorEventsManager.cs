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
}