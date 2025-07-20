using Unity.VisualScripting;
using UnityEngine;

public class GridSpace : MonoBehaviour
{
    // Fields
    [SerializeField] private MachineObject machineObjectPrefab;
    [HideInInspector] public Vector2Int GridPosition;

    private IFillsGridSlot _gridObject;
    public IFillsGridSlot GridObject
    {
        get => _gridObject;
        set
        {
            // delete whatever was previously in the location
            var gridObjectScript = _gridObject as MonoBehaviour;
            if (gridObjectScript != null)
            {
                Destroy(gridObjectScript.gameObject);
            }

            _gridObject = value;
        }
    }
}