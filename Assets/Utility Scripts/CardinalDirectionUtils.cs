using System;
using System.Collections.Generic;
using UnityEngine;

public static class CardinalDirectionUtils
{
    public enum CardinalDirection { Up, Right, Down, Left }
    public static Vector2Int CardinalDirectionVector(CardinalDirection direction)
        {
            return direction switch
            {
                CardinalDirection.Up => Vector2Int.up,
                CardinalDirection.Down => Vector2Int.down,
                CardinalDirection.Left => Vector2Int.left,
                CardinalDirection.Right => Vector2Int.right,
                _ => throw new NotImplementedException(),
            };
        }
    public static CardinalDirection RotateCardinalDirection(CardinalDirection direction, int rotation)
    {
        int newRotationValue = Calculations.Modulo((int)direction + rotation, 4);
        return (CardinalDirection)newRotationValue;
    }
    public static CardinalDirection FlipCardinalDirection(CardinalDirection direction)
    {
        return RotateCardinalDirection(direction, 2);
    }

    [Serializable]
    public struct MultiCardinalDirections
    {
        // Fields
        public bool Up, Right, Down, Left;
        
        // Properties
        public readonly CardinalDirection[] Directions
        {
            get
            {
                List<CardinalDirection> directionVectors = new();
                if (Up) directionVectors.Add(CardinalDirection.Up);
                if (Down) directionVectors.Add(CardinalDirection.Down);
                if (Left) directionVectors.Add(CardinalDirection.Left);
                if (Right) directionVectors.Add(CardinalDirection.Right);
                return directionVectors.ToArray();
            }
        }
        public readonly Vector2Int[] CardinalDirectionVectors
        {
            get
            {
                List<Vector2Int> directionVectors = new();
                foreach (CardinalDirection direction in Directions)
                {
                    directionVectors.Add(CardinalDirectionVector(direction));
                }
                return directionVectors.ToArray();
            }
        }
        
        // Methods
        public readonly CardinalDirection[] RotatedDirections(int turns)
        {
            var rotated = new CardinalDirection[Directions.Length];
            for (int i = 0; i < Directions.Length; i++)
            {
                rotated[i] = RotateCardinalDirection(Directions[i], turns);
            }
            return rotated;
        }
    }
}