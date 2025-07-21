using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite ItemSprite;
    public uint StackSize;
}
