using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsManager : MonoBehaviour
{
    // Properties
    private static GameControls _gameControlsInstance;
    private static GameControls Controls
    {
        get
        {
            _gameControlsInstance ??= new GameControls();
            return _gameControlsInstance;
        }
    }
    
    public static InputAction Interact => Controls.Gameplay.Interact;
    public static InputAction Remove => Controls.Gameplay.Remove;
    public static InputAction PickItem => Controls.Gameplay.PickItem;
    public static InputAction Point => Controls.Gameplay.Point;
    public static InputAction HotbarSelect => Controls.Gameplay.HotbarSelect;
    public static InputAction HotbarChange => Controls.Gameplay.HotbarChange;
    public static InputAction RotateCW => Controls.Gameplay.RotateCW;
    public static InputAction RotateCCW => Controls.Gameplay.RotateCCW;
    public static InputAction ProcessTempSelect => Controls.Gameplay.ProcessTempSelect; //temp

    // Methods
    private void OnEnable()
    {
        Controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        Controls.Gameplay.Disable();
    }
}
