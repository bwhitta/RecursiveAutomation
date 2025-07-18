using System.Linq;
using UnityEngine;
using static CardinalDirectionUtils;

public class MachineObject : MonoBehaviour, IFillsGridSlot, IContainsItem
{
    // Fields
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Machine PlacedMachine;

    public Item ContainedItem { get; set; }

    // Methods
    private void Start()
    {
        spriteRenderer.sprite = PlacedMachine.MachineSprite;
    }

    public void Tick(GridLogic gridLogic, Vector2Int gridPosition)
    {
        PlacedMachine.MachineTick(gridLogic, gridPosition);
    }

    public Item TakeItem()
    {
        var item = ContainedItem;
        ContainedItem = null;
        return item;
    }
    public bool AcceptsItem(Item item, CardinalDirection direction)
    {
        return PlacedMachine.AcceptsAllItems || PlacedMachine.AcceptedItems.Contains(item);
    }
}
