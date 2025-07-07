using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hotbar : MonoBehaviour
{
    // Fields
    [SerializeField] private int totalSlots;
    [SerializeField] private Vector2 slotSpacing;
    [SerializeField] private bool centerSlots;
    
    [SerializeField] private InventorySlot hotbarSlotPrefab;
    [SerializeField] private Machine[] defaultMachines;

    [HideInInspector] public InventorySlot[] Slots;
    public InventorySlot CurrentSlot => Slots[SelectedSlot];

    [SerializeField] private GameObject selectedSlotIndicator;

    // Properties
    private event Events.OnValueChanged<int> SlotSelected;
    private int _selectedSlot;
    public int SelectedSlot
    {
        get => _selectedSlot;
        set
        {
            SlotSelected?.Invoke(_selectedSlot, value);
            _selectedSlot = value;
        }
    }

    // Methods
    private void Start()
    {
        // Set up events
        SlotSelected += OnSlotSelected;
        ControlsManager.HotbarSelect.performed += OnHotbarInput;

        // Instantiate the slot objects
        CreateSlots();

        // Set starting slot
        SelectedSlot = 0;
    }

    private void CreateSlots()
    {
        Vector2[] positions = Alignment.AlignedPoints(totalSlots, Vector2.zero, slotSpacing, centerSlots);
        Slots = new InventorySlot[totalSlots];
        for (int i = 0; i < totalSlots; i++)
        {
            // Create the hotbar slot gameobject
            InventorySlot hotbarSlot = Instantiate(hotbarSlotPrefab, transform.transform);
            hotbarSlot.transform.localPosition = positions[i];

            // Set up what is in the slot
            if (i < defaultMachines.Length)
            {
                hotbarSlot.MachineInSlot = defaultMachines[i];
            }

            // Triggers when the slot is clicked
            hotbarSlot.GetComponent<ClickableObject>().ObjectClicked += OnSlotClicked;

            // Save to an array
            Slots[i] = hotbarSlot;
        }
    }

    private void OnSlotClicked(ClickableObject clickedObject)
    {
        int slotIndex = Array.IndexOf(Slots, clickedObject.GetComponent<InventorySlot>());
        SelectedSlot = slotIndex;
    }
    private void OnHotbarInput(InputAction.CallbackContext context)
    {
        // gets the hotbar slot that the key was pressed for. value is 0 when key is released
        int value = (int)context.ReadValue<float>();
        if (value == 0) return;
        SelectedSlot = value - 1;
    }
    private void OnSlotSelected(int previousValue, int newValue)
    {
        Vector2 slotPosition = Slots[newValue].transform.localPosition;
        selectedSlotIndicator.transform.localPosition = new Vector3(slotPosition.x, slotPosition.y, selectedSlotIndicator.transform.localPosition.z);
    }
}
