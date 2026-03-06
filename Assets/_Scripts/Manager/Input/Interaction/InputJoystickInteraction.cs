using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputJoystickInteraction : MonoBehaviour
{
    private bool debug;

    private void Update()
    {
        DebugInput();
    }

    private void DebugInput()
    {
        if (!debug)
            return;

        var joystick = Joystick.current;

        if (joystick != null)
            foreach (var control in joystick.allControls)
                if (control is ButtonControl button && button.wasPressedThisFrame)
                    Debug.Log($"Joystick Button pressed: {button.name}");
    }

    public InputJoystickInteraction Initialize(bool debug)
    {
        this.debug = debug;

        return this;
    }

    public bool IsButtonHeld(string buttonName)
    {
        var joystick = Joystick.current;

        if (joystick == null) 
            return false;

        var button = GetJoystickButton(joystick, buttonName);
        
        return button != null && button.isPressed;
    }

    public bool IsButtonDown(string buttonName)
    {
        var joystick = Joystick.current;

        if (joystick == null) 
            return false;

        var button = GetJoystickButton(joystick, buttonName);

        return button != null && button.wasPressedThisFrame;
    }

    public bool IsButtonUp(string buttonName)
    {
        var joystick = Joystick.current;

        if (joystick == null) 
            return false;

        var button = GetJoystickButton(joystick, buttonName);

        return button != null && button.wasReleasedThisFrame;
    }

    private ButtonControl GetJoystickButton(Joystick joystick, string buttonName)
    {
        return joystick.TryGetChildControl<ButtonControl>(buttonName);
    }
}