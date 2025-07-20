using UnityEngine;

public class GridSetup : MonoBehaviour
{
    // Fields
    [SerializeField] private GridSpace gridSpacePrefab;
    public float gridSpaceOffset;

    // Properties
    private GridLogic _gridLogicRef;
    private GridLogic GridLogicRef => _gridLogicRef = _gridLogicRef != null ? _gridLogicRef : GetComponent<GridLogic>();
    private GridInputs _gridInputsRef;
    private GridInputs GridInputsRef => _gridInputsRef = _gridInputsRef != null ? _gridInputsRef : GetComponent<GridInputs>();

    // Methods
    private void Start()
    {
        GridOutlineSize();
        CreateGrid();
    }
    private void GridOutlineSize()
    {
        float gridDimensions = GridLogicRef.GridSize * gridSpaceOffset;
        GetComponent<SpriteRenderer>().size = new(gridDimensions, gridDimensions);
    }
    private void CreateGrid()
    {
        GridLogicRef.GridSpaces = new GridSpace[GridLogicRef.GridSize, GridLogicRef.GridSize];

        // calculate positions
        float[] xPositions = Alignment.AlignedPoints(GridLogicRef.GridSize, 0f, gridSpaceOffset, true);
        float[] yPositions = Alignment.AlignedPoints(GridLogicRef.GridSize, 0f, gridSpaceOffset, true);

        for (int gridX = 0; gridX < GridLogicRef.GridSize; gridX++)
        {
            for (int gridY = 0; gridY < GridLogicRef.GridSize; gridY++)
            {
                // Create the gameobject
                GridSpace gridSpace = Instantiate(gridSpacePrefab, transform);
                gridSpace.transform.localPosition = new Vector2(xPositions[gridX], yPositions[gridY]);
                gridSpace.GridPosition = new(gridX, gridY);
                GridLogicRef.GridSpaces[gridX, gridY] = gridSpace;

                // Set up events
                var gridSpaceInputs = gridSpace.GetComponent<GridSpaceInputs>();
                gridSpaceInputs.GridSpaceInput += GridInputsRef.OnGridSpaceInput;
            }
        }
    }
}
