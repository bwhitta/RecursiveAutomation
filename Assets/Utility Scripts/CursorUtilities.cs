using UnityEngine;

public static class CursorUtilities
{
    public static bool MouseHoveringGameObject(GameObject gameObject)
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
}
