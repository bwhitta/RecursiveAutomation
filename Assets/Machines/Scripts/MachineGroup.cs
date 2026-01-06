using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MachineGroup
{
    // could replace the current method of calculating machinesTargetingThis with a dictionary that is created when the constructor is first run
    // the key would be a specific machine, and the value would be an array of every machine that targets it.
    // this would mainly be useful if there are machines that target machines besides their adjacent space

    // to avoid infinite loops once machines have multiple outputs, make it so that when a space's outputs are calculated it actually calculates the possible outputs for each of its directions
    
    // Constructor
    public MachineGroup(GridLogic gridLogic, GridSpace gridSpace)
    {
        GroupRecipe = GetMachineRecipe(gridLogic, gridSpace/*, out ItemStack groupInputs*/);
        Debug.Log($"Final group recipe: {GroupRecipe}");
    }

    // Fields
    public Recipe GroupRecipe;

    // Methods
    // todo: add support for multiple recipes, add support for input bottlenecking
    private Recipe GetMachineRecipe(GridLogic gridLogic, GridSpace gridSpace)
    {
        MachineObject machineObject = gridSpace.GridObject as MachineObject;
        Machine machine = machineObject.PlacedMachine;
        
        Debug.Log($"Checking gridSpace at {gridSpace.GridPosition}", gridSpace);

        // If the machine doesn't need inputs it always outputs.
        if (machine.MachineRecipe.InputItems.Item == null)
        {
            return machine.MachineRecipe;
        }

        // All items that are outputed by machines targeting this
        // (later: return both a minimum outputs which assumes no external inputs, and a maximum output, assuming there is external input. or I need to figure out a way to make a seperate recipe based on each one or something)
        // could essentially have the final recipe say "this recipe accepts up 3 items/sec from either or both of these directions, outputs 2 items/sec when fed, and w/out any inputs this recipe makes 1 items/sec)
        ItemStack inputs = new();
        ItemStack outputs = new();


        // Calculate inputs
        foreach (GridSpace targetingMachine in gridSpace.MachinesTargetingSpace(gridLogic))
        {
            // Get the output of the machine using this same method
            // If the machine needs an input but can't get it, then return null (no recipe)
            // (ADD LATER) If the machine needs an input and it's next to an input space (probably will use the middle left space for now) then return its input and output
            Recipe targetingRecipe = GetMachineRecipe(gridLogic, targetingMachine);
            
            if (targetingRecipe.OutputItems.Item == machine.MachineRecipe.InputItems.Item)
            {
                inputs.Item = targetingRecipe.OutputItems.Item;
                inputs.Quantity += targetingRecipe.OutputItems.Quantity;
            }
        }

        // Calculate outputs based on inputs (cap to bottleneck)
        float percentFulfilled = Mathf.Max(1, inputs.Quantity / machine.MachineRecipe.InputItems.Quantity);

        return new Recipe(inputs, outputs);
    }
}