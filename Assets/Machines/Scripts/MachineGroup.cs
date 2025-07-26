using UnityEngine;
using System.Collections.Generic;
using static CardinalDirectionUtils;

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
        
        bool anyInputAllowed = true;
        bool itemsPassThrough = true; // not sure if this should be true or false by default, nor if it actually matters. probably true?
        List<Recipe> possibleRecipes = new();
        // should start out 
        
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
            UpdatePossibleInputs(machineObject);

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
        void UpdatePossibleInputs(MachineObject machineObject)
        {
            // get the list of possible inputs of a machine
            if (anyInputAllowed)
            {
                // if the machine has anyInputAllowed then change nothing
                // else set the possible inputs to the machine's PossibleInput()
            }
            else
            {
                // if the machine has anyInputAllowed then change nothing
                // same as above, but filter the items
                // when an input is "deconfirmed", somehow figure out how that changes possible outputs
                // could instead calculate possible outputs at the very end, after the whole "chain" is set up
            }
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