using System.Linq;
using UnityEngine;
using static CardinalDirectionUtils;

public class MachineObject : MonoBehaviour, IFillsGridSlot, IContainsItem
{
    // Fields
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Machine PlacedMachine;

    public Item ContainedItem { get; set; }
    
    private int _rotation;
    [HideInInspector] public int Rotation
    {
        get
        {
            return _rotation;
        }
        set
        {
            transform.rotation = Quaternion.Euler(0, 0, value * -90);
            _rotation = value;
        }
    }

    // Methods
    private void Start()
    {
        spriteRenderer.sprite = PlacedMachine.MachineSprite;
    }

    public void Tick(GridLogic gridLogic, Vector2Int gridPosition, int tick)
    {
        PlacedMachine.MachineTick(gridLogic, gridPosition, Rotation, tick);
    }

    public Item TakeItem()
    {
        var item = ContainedItem;
        ContainedItem = null;
        return item;
    }
    public bool AcceptsItem(Item item, CardinalDirection direction)
    {
        bool emptyInventory = ContainedItem == null;
        return emptyInventory && (PlacedMachine.AcceptsAllItems || PlacedMachine.AcceptedItems.Contains(item));
    }
}
