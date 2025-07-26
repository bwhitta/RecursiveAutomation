using UnityEngine;
using System;
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
            Recipe recipe = Recipe.FindRecipe(Recipes, inventory.ContainedItemStack.Item);
            if (recipe == null)
            {
                return;
            }

            // Check if the machine has enough items for the recipe
            if (recipe.InputItems.Item == null || inventory.ContainedItemStack.Quantity < recipe.InputItems.Quantity)
            {
                return;
            }

            // Try to output an item
            CardinalDirection adjustedOutputDirection = RotateCardinalDirection(OutputDirection, rotation);
            Vector2Int targetPosition = gridSpace.GridPosition + CardinalDirectionVector(adjustedOutputDirection);
            if (gridLogic.IsPositionOnGrid(targetPosition))
            {
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
    }
    public override bool AcceptsItem(Item item)
    {
        foreach (var recipe in Recipes)
        {
            if (recipe.AcceptsItem(item))
            {
                return true;
            }
        }
        return false;
    }
    /*public override Item CalculateOutputs(Item inputItem, float inputQuantityPerSecond, out float quantityPerSecond)
    {
        Debug.Log("rates currently are not slowed by having a lack of items");

        float productionsPerSecond = GridTick.TicksPerSecond * TicksPerProduction;
        Recipe usedRecipe = Array.Find(Recipes, MatchingInputItem);

        quantityPerSecond = productionsPerSecond * usedRecipe.OutputItems.Quantity;
        return usedRecipe.OutputItems.Item;

        // Local Methods
        bool MatchingInputItem(Recipe recipe) => recipe.AcceptsItem(inputItem);
    }*/
}
