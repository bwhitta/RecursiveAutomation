using System;

[Serializable]
public class Recipe
{
    // could easily turn both of these into arrays if I wanted something to have multiple inputs and/or outputs
    public ItemStack inputItems;
    public ItemStack outputItems;
}
