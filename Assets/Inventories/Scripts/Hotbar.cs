using UnityEngine;

public class Hotbar : MonoBehaviour
{
    // Fields
    public const int TotalSlots = 8;
    [SerializeField] private Vector2 slotSpacing;
    [SerializeField] private bool centerSlots;
    
    [SerializeField] private HotbarSlot hotbarSlotPrefab;
    [SerializeField] private Machine[] defaultMachines;

    [HideInInspector] public HotbarSlot[] Slots;
    public HotbarSlot CurrentSlot => Slots[SelectedSlot];

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

    private HotbarInputs _hotbarInputsRef;
    public HotbarInputs HotbarInputsRef => _hotbarInputsRef != null ? _hotbarInputsRef : (_hotbarInputsRef = GetComponent<HotbarInputs>());

    // Methods
    private void Start()
    {
        // Set up events
        SlotSelected += OnSlotSelected;

        // Instantiate the slot objects
        CreateSlots();

        // Set starting slot
        SelectedSlot = 0;
    }

    private void CreateSlots()
    {
        Vector2[] positions = Alignment.AlignedPoints(TotalSlots, Vector2.zero, slotSpacing, centerSlots);
        Slots = new HotbarSlot[TotalSlots];
        for (int i = 0; i < TotalSlots; i++)
        {
            // Create the hotbar slot gameobject
            HotbarSlot hotbarSlot = Instantiate(hotbarSlotPrefab, transform.transform);
            hotbarSlot.transform.localPosition = positions[i];

            // Set up what is in the slot
            if (i < defaultMachines.Length)
            {
                hotbarSlot.MachineInSlot = defaultMachines[i];
            }

            // Triggers when the slot is clicked
            hotbarSlot.GetComponent<ClickableObject>().ObjectClicked += HotbarInputsRef.OnSlotClicked;

            // Save to an array
            Slots[i] = hotbarSlot;
        }
    }

    private void OnSlotSelected(int previousValue, int newValue)
    {
        Vector2 slotPosition = Slots[newValue].transform.localPosition;
        selectedSlotIndicator.transform.localPosition = new Vector3(slotPosition.x, slotPosition.y, selectedSlotIndicator.transform.localPosition.z);
    }
}
