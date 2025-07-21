using UnityEngine;
using static CardinalDirectionUtils;

public class DroppedItem : MonoBehaviour, IFillsGridSlot, IContainsItemStack
{
    // Fields
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Properties
    private ItemStack _containedItem;
    public ItemStack ContainedItemStack
    {
        get => _containedItem;
        set
        {
            spriteRenderer.sprite = value.Item.ItemSprite;
            _containedItem = value;
        }
    }

    // Methods
    public bool AcceptsItem(Item item, CardinalDirection direction) => false;
    public void Tick(GridLogic gridLogic, GridSpace gridSpace, int tick) { /* does nothing when ticked */ }
}
