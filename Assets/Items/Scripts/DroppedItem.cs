using UnityEngine;
using static CardinalDirectionUtils;

public class DroppedItem : MonoBehaviour, IFillsGridSlot, IContainsItem
{
    // Fields
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Properties
    private Item _containedItem;
    public Item ContainedItem
    {
        get => _containedItem;
        set
        {
            spriteRenderer.sprite = value.ItemSprite;
            _containedItem = value;
        }
    }

    // Methods
    public bool AcceptsItem(Item item, CardinalDirection direction) => false;
    public Item TakeItem()
    {
        Destroy(gameObject);
        return ContainedItem;
    }

    public void Tick(GridLogic gridLogic, Vector2Int gridPosition) { /* does nothing when ticked */ }
}
