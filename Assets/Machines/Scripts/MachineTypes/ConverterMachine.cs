using UnityEngine;
using static CardinalDirectionUtils;

[CreateAssetMenu(menuName = "Scriptable Objects/Machines/Converter Machine")]
public class ConverterMachine : Machine
{
    public CardinalDirection OutputDirection;
    public Recipe[] recipes;
    public int TicksPerProduction;

    public override void MachineTick(GridLogic gridLogic, GridSpace gridSpace, int rotation, int tick)
    {
        if ((tick % TicksPerProduction) == 0)
        {
            // Get the recipe
            var inventory = gridSpace.GridObject as IContainsItemStack;
            if (inventory.ContainedItemStack.Item == null)
            {
                return;
            }

            Recipe usedRecipe = null;
            foreach (var recipe in recipes)
            {
                if (inventory.ContainedItemStack.Item == recipe.inputItems.Item && inventory.ContainedItemStack.Quantity >= recipe.inputItems.Quantity)
                {
                    usedRecipe = recipe;
                    break;
                }
            }
            if (usedRecipe == null)
            {
                return;
            }

            // Try to output an item
            CardinalDirection adjustedOutputDirection = RotateCardinalDirection(OutputDirection, rotation);
            Vector2Int targetPosition = gridSpace.GridPosition + CardinalDirectionVector(adjustedOutputDirection);
            if (gridLogic.IsPositionOnGrid(targetPosition))
            {
                bool successfulOutput = ItemManagement.OutputItems(gridLogic, targetPosition, usedRecipe.outputItems, adjustedOutputDirection, out _);
                if (successfulOutput)
                {
                    // Consume the item
                    ItemStack newItemStack = inventory.ContainedItemStack;
                    newItemStack.Quantity -= usedRecipe.inputItems.Quantity;
                    inventory.ContainedItemStack = newItemStack;
                }
            }
        }
    }
}
