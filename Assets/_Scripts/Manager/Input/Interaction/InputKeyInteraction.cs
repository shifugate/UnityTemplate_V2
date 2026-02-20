using UnityEngine;

namespace Assets._Scripts.Manager.Input.Interaction
{
    public class InputKeyInteraction : MonoBehaviour
    {
        public bool IsKeyHeld(string keyName)
        {
            if (UnityEngine.InputSystem.Keyboard.current == null)
                return false;

            if (!global::System.Enum.TryParse(keyName, true, out UnityEngine.InputSystem.Key key))
                return false;

            return UnityEngine.InputSystem.Keyboard.current[key].isPressed;
        }

        public bool IsKeyDown(string keyName)
        {
            if (UnityEngine.InputSystem.Keyboard.current == null)
                return false;

            if (!global::System.Enum.TryParse(keyName, true, out UnityEngine.InputSystem.Key key))
                return false;

            return UnityEngine.InputSystem.Keyboard.current[key].wasPressedThisFrame;
        }

        public bool IsKeyUp(string keyName)
        {
            if (UnityEngine.InputSystem.Keyboard.current == null)
                return false;

            if (!global::System.Enum.TryParse(keyName, true, out UnityEngine.InputSystem.Key key))
                return false;

            return UnityEngine.InputSystem.Keyboard.current[key].wasReleasedThisFrame;
        }
    }
}
