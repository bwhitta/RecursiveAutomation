using UnityEngine;

public static class Calculations
{
    /// <summary>
    /// Calculates the remainder after dividing numberToModify by the modifyingNumber
    /// </summary>
    public static float Modulo(float numberToModify, float modifyingNumber)
    {
        return numberToModify - (modifyingNumber * Mathf.Floor(numberToModify / modifyingNumber));
    }
    public static int Modulo(int numberToModify, int modifyingNumber)
    {
        return numberToModify - (modifyingNumber * (int)Mathf.Floor((float)numberToModify / modifyingNumber));
    }
}
