using System.Collections.Generic;
using UnityEngine;

public class ControlsServiceLocator : MonoBehaviour // ServiceLocator
{
    private readonly Dictionary<InputDeviceType, IControlsService> controlsServices = new();

    public void Awake()
    {
        // Possibly move service registration to some bootstrap script
        RegisterService(new GamepadControlsService());
        RegisterService(new KeyboardControlsService());
    }

    public IControlsService GetService(InputDeviceType inputDeviceType)
    {
        return controlsServices[inputDeviceType];
    }

    public IControlsService this[InputDeviceType inputDeviceType] => GetService(inputDeviceType);

    public void RegisterService(IControlsService controlsServiceService)
    {
        controlsServices[controlsServiceService.InputDeviceType] = controlsServiceService;
    }

    public void UnregisterService(IControlsService controlsServiceService)
    {
        controlsServices.Remove(controlsServiceService.InputDeviceType);
    }
}

public enum InputDeviceType
{
    Keyboard,
    Gamepad
}

public interface IControlsService
{
    public InputDeviceType InputDeviceType { get; }
    public bool JumpButtonDown { get; }
    public bool MainMenuButtonDown { get; }
    public bool InteractButtonDown { get; }
    public float HorizontalAxis { get; }
}

public class GamepadControlsService : IControlsService
{
    public InputDeviceType InputDeviceType => InputDeviceType.Gamepad;
    public bool JumpButtonDown => Input.GetKeyDown(KeyCode.Joystick1Button0);
    public bool MainMenuButtonDown => Input.GetKeyDown(KeyCode.Joystick1Button7);
    public bool InteractButtonDown => Input.GetKeyDown(KeyCode.Joystick1Button2);
    public float HorizontalAxis => Input.GetAxis("GamepadHorizontal");
}

public class KeyboardControlsService : IControlsService
{
    public InputDeviceType InputDeviceType => InputDeviceType.Keyboard;
    public bool JumpButtonDown => Input.GetKeyDown(KeyCode.Space);
    public bool MainMenuButtonDown => Input.GetKeyDown(KeyCode.Escape);
    public bool InteractButtonDown => Input.GetKeyDown(KeyCode.E);
    public float HorizontalAxis => Input.GetAxis("KeyboardHorizontal");
}