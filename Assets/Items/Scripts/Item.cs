using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    // could turn into an abstract class, mainly depends on if different items end up having different functionalities (or if they are all just generic "resources" used by the machine)
    public string ItemName;
    public Sprite ItemSprite;
}
