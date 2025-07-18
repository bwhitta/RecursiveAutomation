using UnityEngine;

public class GridSpace : MonoBehaviour
{
    // Fields
    [SerializeField] private MachineObject machineObjectPrefab;
    [HideInInspector] public Vector2Int GridPosition;

    // should make SlotDroppedItem and SlotMachineObject both based on either the same interface or the same abstract class
    // rather than using complex BS to detect if it has an inventory slot, literally just have a variable called InventorySlot and it's set to null if it can't hold items
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
                Debug.Log("clearing whatever was previously in the grid space");
                Destroy(gridObjectScript.gameObject);
            }

            _gridObject = value;
        }
    }

    // possibly add something here that gives a reference to any InventorySlot in the space
}