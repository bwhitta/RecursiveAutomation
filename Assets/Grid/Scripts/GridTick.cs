using UnityEngine;

public class GridTick : MonoBehaviour
{
    public float ticksPerSecond;
    [SerializeField] private GridLogic gridLogic;
    public float TimeSinceLastTick { get; private set; }

    // could move to FixedUpdate and make fixed update speed equal to ticks per second
    private void Update()
    {
        TimeSinceLastTick += Time.deltaTime;
        if (TimeSinceLastTick > (1f / ticksPerSecond))
        {
            TimeSinceLastTick -= 1f / ticksPerSecond;
            TickAllMachines();
        }
    }

    private void TickAllMachines()
    {
        foreach(GridSpace gridSpace in gridLogic.GridSpaces)
        {
            // Ticks whatever is in the grid space if it's not null
            gridSpace.GridObject?.Tick(gridLogic, gridSpace.GridPosition);
        }
    }
}
