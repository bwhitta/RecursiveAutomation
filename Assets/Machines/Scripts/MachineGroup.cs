using UnityEngine;
using System.Collections.Generic;
using static CardinalDirectionUtils;
using System.Linq;

public class MachineGroup
{
    // Constructor
    public MachineGroup(GridLogic gridLogic, GridSpace startingSpace)
    {
        if (startingSpace.GridObject as MachineObject == null)
        {
            Debug.LogWarning("starting space is not a machine object!");
            return;
        }

        List<GridSpace> gridSpaces = new();
        List<Recipe> machineGroupRecipes = null;
        
        CheckSpace(startingSpace);

        // display what machines were found (temp)
        DisplayGridSpaces();
        
        // Local Methods
        void CheckSpace(GridSpace gridSpace)
        {
            Debug.Log($"checking space ({gridSpace.GridPosition.x}, {gridSpace.GridPosition.y})", gridSpace.gameObject);
            gridSpaces.Add(gridSpace);

            // update possibleInputItems and anyInputAllowed
            var machineObject = gridSpace.GridObject as MachineObject;
            UpdateGroupRecipes(machineObject);

            // Check each space that outputs to gridSpace (if it hasn't already been checked)
            List<GridSpace> linkedSpaces = LinkedAdjacentSpaces(gridSpace);
            foreach (var linkedSpace in linkedSpaces)
            {
                Debug.Log($"linked space found!", linkedSpace.gameObject);
                if (!gridSpaces.Contains(linkedSpace))
                {
                    CheckSpace(linkedSpace);
                }
            }
        }
        
        void UpdateGroupRecipes(MachineObject machineObject)
        {
            // this should somehow simulate for each machine pointing into a machine simultaneously. maybe take an array of machines as an input?

            if (machineGroupRecipes == null)
            {
                machineGroupRecipes = machineObject.PlacedMachine.Recipes.ToList();
                return;
            }

            // Find machine recipes with outputs that match a group recipe's 
            foreach (var machineRecipe in machineObject.PlacedMachine.Recipes)
            {
                for (int i = 0; i < machineGroupRecipes.Count; i++)
                {
                    if (RecipesMatch(machineRecipe, machineGroupRecipes[i]))
                    {
                        Debug.Log($"recipes match! machine: {machineRecipe} IF THIS DOESN'T LOOK RIGHT OVERRIDE TOSTRING IN RECIPE", machineObject.gameObject);
                        machineGroupRecipes[i] = CombineRecipes(machineRecipe, machineGroupRecipes[i]);
                        Debug.Log($"new recipe: {machineGroupRecipes[i]}", machineObject.gameObject);
                    }
                }
            }
        }
        bool RecipesMatch(Recipe machineRecipe, Recipe groupRecipe)
        {
            // check if the groupRecipe's output matches the machineRecipe's input
            return groupRecipe.AcceptsItem(machineRecipe.OutputItems.Item);
        }
        Recipe CombineRecipes(Recipe machineRecipe, Recipe groupRecipe)
        {
            ItemStack newInputs = machineRecipe.InputItems;
            ItemStack newOutputs = groupRecipe.OutputItems;
            Recipe newRecipe = new(newInputs, newOutputs);

            return newRecipe;
        }

        List<GridSpace> LinkedAdjacentSpaces(GridSpace gridSpace)
        {
            List<GridSpace> linkedSpaces = new();
            List<GridSpace> adjacentSpaces = gridSpace.AdjacentSpaces(gridLogic, gridSpace);
            foreach (var adjacentSpace in adjacentSpaces)
            {
                // check if the adjacent space has a machine that targets this one
                if (adjacentSpace.GridObject != null && GridSpaceOutputTarget(adjacentSpace) == gridSpace)
                {
                    linkedSpaces.Add(adjacentSpace);
                }
            }
            return linkedSpaces;
        }
        GridSpace GridSpaceOutputTarget(GridSpace gridSpace)
        {
            var machineObject = gridSpace.GridObject as MachineObject;
            if (machineObject != null)
            {
                CardinalDirection outputDirection = RotateCardinalDirection(machineObject.PlacedMachine.OutputDirection, machineObject.Rotation);
                Vector2Int gridPosition = gridSpace.GridPosition + CardinalDirectionVector(outputDirection);
                return gridLogic.GridSpaces[gridPosition.x, gridPosition.y];
            }
            else
            {
                return null;
            }
        }
        // temporary method for visualization
        void DisplayGridSpaces()
        {
            foreach (var space in gridSpaces)
            {
                space.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
    }

    // eventually will want to turn the outputs into a dictionary, with the item as a key and the items/sec as the value
    public Item GroupOutputs;
    public float OutputItemsPerSecond;
    
    // probably should eventually make this and MachineObjects derive from a common interface that has CalculateOutputs, probably called IOutputsItems
    // as well as both deriving from IAcceptsItems (which should be split off from IContainsItems)
}