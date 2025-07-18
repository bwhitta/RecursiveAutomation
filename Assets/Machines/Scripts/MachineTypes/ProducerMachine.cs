using UnityEngine;
using static CardinalDirectionUtils;

[CreateAssetMenu(menuName = "Scriptable Objects/Machines/Producer Machine")]
public class ProducerMachine : Machine
{
    public MultiCardinalDirections OutputDirections;
    public Item ProducedItem;
    public int TicksPerProduction;
    // public int ItemsPerProduction;

    private int ticksSinceProduction;
    public override void MachineTick(GridLogic gridLogic, Vector2Int gridPosition)
    {
        // maybe replace gridPosition with a reference to the original GridObject or something?
        ticksSinceProduction++;
        if (ticksSinceProduction >= TicksPerProduction)
        {
            ticksSinceProduction = 0;
            foreach (CardinalDirection direction in OutputDirections.Directions)
            {
                Vector2Int targetPosition = gridPosition + CardinalDirectionVector(direction);
                bool sucessfulOutput = OutputItem(gridLogic, targetPosition, ProducedItem, direction);
                if (sucessfulOutput)
                {
                    Debug.Log($"successfully outputted item in direction {direction}");
                    break;
                }
            }
        }
    }
}
