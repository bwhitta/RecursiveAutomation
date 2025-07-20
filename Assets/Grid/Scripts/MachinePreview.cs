using UnityEngine;
using UnityEngine.InputSystem;

public class MachinePreview : MonoBehaviour
{
    // Fields
    [SerializeField] private GridInputs gridInputs;
    [SerializeField] private Hotbar hotbar;
    [SerializeField] private SpriteRenderer machinePreview;
    private GridSpace hoveredGridSpace;

    // Properties
    [HideInInspector] private int _previewRotation;
    public int PreviewRotation
    {
        get => _previewRotation;
        set => _previewRotation = Calculations.Modulo(value, 4);
    }

    // Methods
    private void Start()
    {
        gridInputs.GridSpaceHovered += Hovering;
        CursorEventsManager.Instance.HoveringEnded += HoveringEnded;
        
        ControlsManager.RotateCW.started += OnRotateCWInput;
        ControlsManager.RotateCCW.started += OnRotateCCWInput;
    }
    private void Update()
    {
        UpdateMachinePreview();
    }
    private void Hovering(GridSpace gridSpace)
    {
        hoveredGridSpace = gridSpace;
    }
    private void HoveringEnded()
    {
        hoveredGridSpace = null;
    }
    private void UpdateMachinePreview()
    {
        if (hoveredGridSpace != null && hotbar.CurrentSlot.MachineInSlot != null)
        {
            machinePreview.enabled = true;
            machinePreview.transform.SetPositionAndRotation(hoveredGridSpace.transform.position, Quaternion.Euler(0, 0, PreviewRotation * -90));
            machinePreview.sprite = hotbar.CurrentSlot.MachineInSlot.MachineSprite;
        }
        else
        {
            machinePreview.enabled = false;
        }
    }

    private void OnRotateCWInput(InputAction.CallbackContext context)
    {
        PreviewRotation++;
    }
    private void OnRotateCCWInput(InputAction.CallbackContext context)
    {
        PreviewRotation--;
    }

}
