using System.Linq;
using UnityEngine;
using static CardinalDirectionUtils;

public class MachineObject : MonoBehaviour, IFillsGridSlot, IContainsItemStack
{
    // Fields
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Machine PlacedMachine;

    public ItemStack ContainedItemStack { get; set; }

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
        // handy for debugging, and shouldn't really add any lag.
        gameObject.name = PlacedMachine.name + "Object";
        spriteRenderer.sprite = PlacedMachine.MachineSprite;
    }

    public void Tick(GridLogic gridLogic, GridSpace gridSpace, int tick)
    {
        PlacedMachine.MachineTick(gridLogic, gridSpace, Rotation, tick);
    }

    public bool AcceptsItem(Item item, CardinalDirection direction)
    {
        bool itemTypeAccepted = PlacedMachine.AcceptsAllItems || PlacedMachine.AcceptedItems.Contains(item);
        bool itemTypeFits = ContainedItemStack.Item == null || ContainedItemStack.Item == item || ContainedItemStack.Quantity < ContainedItemStack.Item.StackSize;

        CardinalDirection inputSide = FlipCardinalDirection(direction);
        CardinalDirection[] inputDirections = PlacedMachine.InputDirections.RotatedDirections(Rotation);
        bool acceptableDirection = inputDirections.Contains(inputSide);

        return itemTypeAccepted && acceptableDirection && itemTypeFits;
    }
}
