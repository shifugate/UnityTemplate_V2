using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputGamepadInteraction : MonoBehaviour
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

        var gamepad = Gamepad.current;

        if (gamepad != null)
            foreach (var control in gamepad.allControls)
                if (control is ButtonControl button && button.wasPressedThisFrame)
                    Debug.Log($"Gamepad Button pressed: {button.name}");
    }

    public InputGamepadInteraction Initialize(bool debug)
    {
        this.debug = debug;

        return this;
    }

    public bool IsButtonHeld(string buttonName)
    {
        var gamepad = Gamepad.current;

        if (gamepad == null) 
            return false;

        var button = GetGamepadButton(gamepad, buttonName);
        
        return button != null && button.isPressed;
    }

    public bool IsButtonDown(string buttonName)
    {
        var gamepad = Gamepad.current;

        if (gamepad == null) 
            return false;

        var button = GetGamepadButton(gamepad, buttonName);

        return button != null && button.wasPressedThisFrame;
    }

    public bool IsButtonUp(string buttonName)
    {
        var gamepad = Gamepad.current;

        if (gamepad == null) 
            return false;

        var button = GetGamepadButton(gamepad, buttonName);

        return button != null && button.wasReleasedThisFrame;
    }

    private ButtonControl GetGamepadButton(Gamepad gamepad, string buttonName)
    {
        return gamepad.TryGetChildControl<ButtonControl>(buttonName);
    }
}