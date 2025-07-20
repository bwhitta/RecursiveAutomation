using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CursorEvents : MonoBehaviour
{
    public delegate void OnHover();
    public OnHover Hovered;

    private void Start()
    {
        CursorEventsManager.Instance.CursorEvents.Add(this);
    }
}
