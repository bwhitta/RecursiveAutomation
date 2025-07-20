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

    [Serializable]
    public struct MultiCardinalDirections
    {
        public bool Up, Down, Left, Right;
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
        public readonly bool IncludesDirection(CardinalDirection direction)
        {
            return Array.IndexOf(Directions, direction) != -1;
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
    }
}