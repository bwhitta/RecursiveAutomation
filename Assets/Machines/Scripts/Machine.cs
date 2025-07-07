
using UnityEngine;

public abstract class Machine : ScriptableObject
{
    public string machineName;
    public Sprite sprite;
    
    public abstract void MachineTick();
    // public abstract Item CalculateOutputs();
    // public abstract Item CalculateInputs();
}
