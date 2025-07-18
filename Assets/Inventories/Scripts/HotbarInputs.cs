using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarInputs : MonoBehaviour
{
    [SerializeField] private Hotbar hotbar;

    private void Start()
    {
        // Set up events
        ControlsManager.HotbarSelect.performed += OnHotbarSelect;
        ControlsManager.HotbarChange.performed += OnHotbarChange;
    }

    private void OnHotbarSelect(InputAction.CallbackContext context)
    {
        // gets the hotbar slot that the key was pressed for. value is 0 when key is released
        int value = (int)context.ReadValue<float>();
        if (value == 0) return;
        hotbar.SelectedSlot = value - 1;
    }
    public void OnSlotClicked(ClickableObject clickedObject)
    {
        hotbar.SelectedSlot = Array.IndexOf(hotbar.Slots, clickedObject.GetComponent<HotbarSlot>());
    }
    private void OnHotbarChange(InputAction.CallbackContext context)
    {
        int newSlotIndex = hotbar.SelectedSlot + (int)context.ReadValue<float>();
        hotbar.SelectedSlot = Calculations.Modulo(newSlotIndex, hotbar.Slots.Length);
    }
}