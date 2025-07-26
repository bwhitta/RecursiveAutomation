using UnityEngine;
using System;

[Serializable]
public class Recipe
{
    // could make some of this stuff private to avoid misuse and instead make things use methods

    public bool AnyInputPossible;
    public bool ItemsPassThrough;
    public enum ItemProcessMode { Normal, AnyInputPossible, ItemsPassThrough }

    public ItemProcessMode ProcessMode;
    public ItemStack InputItems;
    public ItemStack OutputItems;

    public bool AcceptsItem(Item item)
    {
        if (item == null) Debug.LogError("Item should not be null here");
        return AnyInputPossible || InputItems.Item == item;
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
}
