using UnityEngine;

public class GridTick : MonoBehaviour
{
    // Constants
    public const float TicksPerSecond = 10;
    public static float SecondsPerTick => 1f / TicksPerSecond;

    // Fields
    [SerializeField] private GridLogic gridLogic;
    [HideInInspector] public int TickCount;
    public float TimeSinceLastTick { get; private set; }

    // Methods
    private void Update()
    {
        // could move to FixedUpdate and make fixed update speed equal to ticks per second
        TimeSinceLastTick += Time.deltaTime;
        if (TimeSinceLastTick > (1f / TicksPerSecond))
        {
            TimeSinceLastTick -= 1f / TicksPerSecond;
            TickCount = (TickCount + 1) % int.MaxValue;
            TickAllMachines();
        }
    }

    private void TickAllMachines()
    {
        foreach(GridSpace gridSpace in gridLogic.GridSpaces)
        {
            gridSpace.Tick(gridLogic, TickCount);
        }
    }
}
