using System;
using System.Collections.Generic;

[Serializable]
public class Recipe
{
    // Constructor
    public Recipe(ItemStack inputItems, ItemStack outputItems)
    {
        InputItems = inputItems;
        OutputItems = outputItems;
    }

    // Fields
    public ItemStack InputItems;
    public ItemStack OutputItems;
    // disabled for now while figuring this out, will re-enable when adding conveyors
    // public bool AnyInputPossible;
    // public bool ItemsPassThrough;

    // Methods
    public static Recipe FindRecipe(IEnumerable<Recipe> recipes, Item inputItem)
    {
        foreach (var recipe in recipes)
        {
            if (recipe.InputItems.Item == null || recipe.InputItems.Item == inputItem)
            {
                return recipe;
            }
        }
        return default;
    }
    
    public override string ToString()
    {
        return $"Inputs: {InputItems}, Outputs: {OutputItems}";
    }
}