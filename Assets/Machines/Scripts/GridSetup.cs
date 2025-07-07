using UnityEngine;

public class GridSetup : MonoBehaviour
{
    // Fields
    [SerializeField] private int gridSize;
    [SerializeField] private MachineObject gridSpacePrefab;
    [SerializeField] private float gridSpaceOffset;

    // Properties
    private MachineGrid _machineGridRef;
    private MachineGrid MachineGridRef
    {
        get
        {
            return _machineGridRef = _machineGridRef != null ? _machineGridRef : GetComponent<MachineGrid>();
        }
    }

    // Methods
    void Start()
    {
        GridOutlineSize();
        CreateGrid();
    }
    private void GridOutlineSize()
    {
        float gridDimensions = gridSize * gridSpaceOffset;
        GetComponent<SpriteRenderer>().size = new(gridDimensions, gridDimensions);
    }
    private void CreateGrid()
    {
        MachineGridRef.Machines = new MachineObject[gridSize, gridSize];

        // calculate positions
        float[] xPositions = Alignment.AlignedPoints(gridSize, 0f, gridSpaceOffset, true);
        float[] yPositions = Alignment.AlignedPoints(gridSize, 0f, gridSpaceOffset, true);

        for (int gridX = 0; gridX < gridSize; gridX++)
        {
            for (int gridY = 0; gridY < gridSize; gridY++)
            {
                // Create the gameobject
                MachineObject machineObject = Instantiate(gridSpacePrefab, transform);
                machineObject.transform.localPosition = new Vector2(xPositions[gridX], yPositions[gridY]);
                MachineGridRef.Machines[gridX, gridY] = machineObject;

                // Set up the clickable grid space's event
                var clickableGridSpace = machineObject.GetComponent<GridSpaceInputs>();
                clickableGridSpace.GridSpaceInput += MachineGridRef.OnGridSpaceClicked;
                clickableGridSpace.GridSpacePosition = new(gridX, gridY);
            }
        }
    }
}
