using System;
using UnityEngine;

public static class Alignment
{
    public static float[] AlignedPoints(int amountOfPoints, float basePosition, float offset, bool centerPoints)
    {
        // Calculate the midpoint and adjust the starting point accordingly if necessary
        float startingPoint = basePosition;
        if (centerPoints)
        {
            startingPoint = basePosition - (offset * (amountOfPoints-1) / 2);
        }

        // Create the points
        float[] points = new float[amountOfPoints];
        float currentPoint = startingPoint;
        for (int i = 0; i < amountOfPoints; i++)
        {
            points[i] = currentPoint;
            currentPoint += offset;
        }

        return points;
    }
    public static Vector2[] AlignedPoints(int amountOfPoints, Vector2 basePosition, Vector2 offset, bool centerPoints)
    {
        // Calculate the midpoint and adjust the starting point accordingly if necessary
        Vector2 startingPoint = basePosition;
        if (centerPoints)
        {
            startingPoint = basePosition - (offset * (amountOfPoints-1) / 2);
        }

        // Create the points
        Vector2[] points = new Vector2[amountOfPoints];
        Vector2 currentPoint = startingPoint;
        for (int i = 0; i < amountOfPoints; i++)
        {
            points[i] = currentPoint;
            currentPoint += offset;
        }

        return points;
    }

    internal static object AlignedPoints()
    {
        throw new NotImplementedException();
    }

    /*float leastX = Mathf.Min(startingPosition.x, finalPosition.x);
     float mostX = Mathf.Max(startingPosition.x, finalPosition.x);
    float leastY = Mathf.Min(startingPosition.y, finalPosition.y);
    float mostY = Mathf.Max(startingPosition.y, finalPosition.y);*/
}
