using UnityEngine;
using EventUtils;

public class MachinePreview : MonoBehaviour
{
    // Events
    public event OnValueChanged<int> RotationChanged;

    // Fields
    [SerializeField] private SpriteRenderer machinePreviewObject;
    [SerializeField] private GridLogic gridLogic;
    [SerializeField] private Hotbar hotbar;

    // Properties
    [HideInInspector] private int _previewRotation;
    public int PreviewRotation
    {
        get => _previewRotation;
        set
        {
            int previousValue = _previewRotation;
            _previewRotation = Calculations.Modulo(value, 4);
            RotationChanged?.Invoke(previousValue, value);
        }
    }

    private void Start()
    {
        CursorEventsManager.Instance.HoveringEnded += HoveringEnded;
        hotbar.SlotSelected += RefreshVisuals;
        RotationChanged += PreviewRotationChanged;
    }
    public void OnMouseHoverMachine(GridSpace gridSpace)
    {
        machinePreviewObject.transform.position = gridLogic.GridSpaces[gridSpace.GridPosition.x, gridSpace.GridPosition.y].transform.position;
        RefreshVisuals(default, hotbar.SelectedSlot);
    }
    // set it up so that this is called every time the rotation changes (will need to set up an event for that probably)
    public void PreviewRotationChanged(int previousValue, int newValue)
    {
        RefreshVisuals(default, hotbar.SelectedSlot);
    }
    public void RefreshVisuals(int _, int newValue)
    {
        if (hotbar.Slots[newValue].MachineInSlot != null)
        {
            machinePreviewObject.enabled = true;
            machinePreviewObject.sprite = hotbar.Slots[newValue].MachineInSlot.MachineSprite;
            machinePreviewObject.transform.rotation = Quaternion.Euler(0, 0, PreviewRotation * -90);
        }
        else
        {
            machinePreviewObject.enabled = false;
        }
    }
    private void HoveringEnded()
    {
        machinePreviewObject.enabled = false;
    }
}
