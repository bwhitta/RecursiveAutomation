using UnityEngine;
using static CardinalDirectionUtils;

[CreateAssetMenu(menuName = "Scriptable Objects/Machines/Producer Machine")]
public class ProducerMachine : Machine
{
    public CardinalDirection OutputDirection;
    public Item ProducedItem;
    public int TicksPerProduction;
    // public int ItemsPerProduction;

    public override void MachineTick(GridLogic gridLogic, Vector2Int gridPosition, int rotation, int tick)
    {
        // could instead make it so that the machine takes a certain amount of ticks to run (rather than running on an exact tick), and just only resets its value if it successfully outputs. Would need to track tick seperately on the MachineObject though
        if ((tick % TicksPerProduction) == 0)
        {
            CardinalDirection adjustedOutputDirection = RotateCardinalDirection(OutputDirection, rotation);
            
            // Try to output an item
            Vector2Int targetPosition = gridPosition + CardinalDirectionVector(adjustedOutputDirection);
            if (gridLogic.IsPositionOnGrid(targetPosition))
            {
                ItemManagement.OutputItem(gridLogic, targetPosition, ProducedItem, adjustedOutputDirection);
            }
        }
    }
}
