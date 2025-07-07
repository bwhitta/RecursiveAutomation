using UnityEngine;

public class MachineObject : MonoBehaviour
{
    // Fields
    private Machine _placedMachine;
    public Machine PlacedMachine
    {
        get => _placedMachine;
        set
        {
            _placedMachine = value;
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
        if (PlacedMachine == null)
        {
            SpriteRendererRef.sprite = null;
        }
        else
        {
            SpriteRendererRef.sprite = PlacedMachine.sprite;
        }
    }
}
