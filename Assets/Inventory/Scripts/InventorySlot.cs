using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    // Fields
    private Machine _machineInSlot;
    public Machine MachineInSlot
    {
        get => _machineInSlot;
        set
        {
            _machineInSlot = value;
            MachineChanged();
        }
    }

    private SpriteRenderer _spriteRendererRef;
    private SpriteRenderer SpriteRendererRef
    {
        get
        {
            return _spriteRendererRef = _spriteRendererRef != null ? _spriteRendererRef : GetComponent<SpriteRenderer>();
        }
    }

    // Methods
    private void MachineChanged()
    {
        if (MachineInSlot == null)
        {
            SpriteRendererRef.sprite = null;
        }
        else
        {
            SpriteRendererRef.sprite = MachineInSlot.sprite;
        }

    }
}
