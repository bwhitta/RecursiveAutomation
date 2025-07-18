using UnityEngine;
using UnityEngine.InputSystem;

public static class ControlsManager
{
    private static GameControls _gameControlsInstance;
    private static GameControls Controls
    {
        get
        {
            _gameControlsInstance ??= new GameControls();
            return _gameControlsInstance;
        }
    }

    // Get specific controls
    public static InputAction Interact => GetInputAction(Controls.Gameplay.Interact);
    public static InputAction Remove => GetInputAction(Controls.Gameplay.Remove);
    public static InputAction PickItem => GetInputAction(Controls.Gameplay.PickItem);
    public static InputAction Point => GetInputAction(Controls.Gameplay.Point);
    public static InputAction HotbarSelect => GetInputAction(Controls.Gameplay.HotbarSelect);
    public static InputAction HotbarChange => GetInputAction(Controls.Gameplay.HotbarChange);

    private static InputAction GetInputAction(InputAction inputAction)
    {
        if (!inputAction.enabled)
        {
            inputAction.Enable();
        }
        return inputAction;
    }
}
