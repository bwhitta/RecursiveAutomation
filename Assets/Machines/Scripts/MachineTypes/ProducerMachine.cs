using UnityEngine;
using static CardinalDirectionUtils;

[CreateAssetMenu(menuName = "Scriptable Objects/Machines/Producer Machine")]
public class ProducerMachine : Machine
{
    public CardinalDirection OutputDirection;
    public int TicksPerProduction;

    public ItemStack ProducedItems;

    public override void MachineTick(GridLogic gridLogic, GridSpace gridSpace, int rotation, int tick)
    {
        // could instead make it so that the machine takes a certain amount of ticks to run (rather than running on an exact tick), and just only resets its value if it successfully outputs. Would need to track tick seperately on the MachineObject though
        if ((tick % TicksPerProduction) == 0)
        {
            // Try to output an item
            CardinalDirection adjustedOutputDirection = RotateCardinalDirection(OutputDirection, rotation);
            Vector2Int targetPosition = gridSpace.GridPosition + CardinalDirectionVector(adjustedOutputDirection);
            if (gridLogic.IsPositionOnGrid(targetPosition))
            {
                ItemManagement.OutputItems(gridLogic, targetPosition, ProducedItems, adjustedOutputDirection, out _);
            }
        }
    }
}
