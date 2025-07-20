using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class CursorUtilities
{
    public static bool MouseHovering(GameObject gameObject)
    {
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(ControlsManager.Point.ReadValue<Vector2>()));

        if (rayHit.collider != null) {
            return rayHit.collider.gameObject == gameObject;
        }
        else
        {
            return false;
        }
    }
    // may want to replace GameObject[] with Collider2D[] here, or more likely with a reference to a custom CursorOver class
    public static CursorEvents MouseHovering(List<CursorEvents> cursorEventsObjects)
    {
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(ControlsManager.Point.ReadValue<Vector2>()));
        if (rayHit.collider != null)
        {
            // Find which cursorEvent has a corresponding gameobject
            foreach (CursorEvents cursorEvent in cursorEventsObjects)
            {
                if (rayHit.collider.gameObject == cursorEvent.gameObject)
                {
                    return cursorEvent;
                }
            }
        }
        return null;
    }
}
