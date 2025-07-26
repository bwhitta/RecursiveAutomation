using UnityEngine;
using System;

[Serializable]
public class Recipe
{
    public Recipe(ItemStack inputItems, ItemStack outputItems)
    {
        InputItems = inputItems;
        OutputItems = outputItems;
    }

    // could make some of this stuff private to avoid misuse and instead make things use methods

    // disabled for now while figuring this out, will re-enable when adding conveyors
    // public bool AnyInputPossible;
    // public bool ItemsPassThrough;

    public ItemStack InputItems;
    public ItemStack OutputItems;

    public bool AcceptsItem(Item item)
    {
        return InputItems.Item == item;
    }
    public static Recipe FindRecipe(Recipe[] recipes, Item inputItem)
    {
        foreach (var recipe in recipes)
        {
            if (recipe.AcceptsItem(inputItem))
            {
                return recipe;
            }
        }
        return null;
    }

    // for debugging purposes
    public override string ToString()
    {
        // goal: 
        return $"Inputs: {InputItems}, Outputs: {OutputItems}";
    }
}
