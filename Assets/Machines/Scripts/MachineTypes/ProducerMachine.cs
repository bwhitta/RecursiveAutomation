using UnityEngine;
using static CardinalDirectionUtils;

[CreateAssetMenu(menuName = "Scriptable Objects/Machines/Converter Machine")]
public class ProducerMachine : Machine
{
    // Fields
    public int TicksPerProduction;

    // Methods
    public override void MachineTick(GridLogic gridLogic, GridSpace gridSpace, int rotation, int tick)
    {
        if ((tick % TicksPerProduction) == 0)
        {
            var inventory = gridSpace.GridObject as IContainsItemStack;
            
            // Check if the machine has enough items for the recipe
            if (inventory.ContainedItemStack.Quantity < MachineRecipe.InputItems.Quantity && MachineRecipe.InputItems.Item != null)
            {
                Debug.Log($"not enough items to use recipe");
                return;
            }
            TryOutputItem(MachineRecipe, inventory);
        }

        // Local Methods
        void TryOutputItem(Recipe recipe, IContainsItemStack inventory)
        {
            // Try to output an item
            CardinalDirection adjustedOutputDirection = RotateCardinalDirection(OutputDirection, rotation);
            Vector2Int targetPosition = gridSpace.GridPosition + CardinalDirectionVector(adjustedOutputDirection);
            if (!gridLogic.IsPositionOnGrid(targetPosition))
            {
                return;
            }

            bool successfulOutput = ItemManagement.OutputItems(gridLogic, targetPosition, recipe.OutputItems, adjustedOutputDirection, out _);
            if (successfulOutput)
            {
                // Consume the item
                ItemStack newItemStack = inventory.ContainedItemStack;
                newItemStack.Quantity -= recipe.InputItems.Quantity;
                inventory.ContainedItemStack = newItemStack;
            }
        }
    }
    public override bool AcceptsItem(Item item)
    {
        return MachineRecipe.InputItems.Item == item;
    }
}
