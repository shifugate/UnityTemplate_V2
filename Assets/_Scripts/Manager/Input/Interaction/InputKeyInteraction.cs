using UnityEngine;
using UnityEngine.InputSystem.Controls;

namespace Assets._Scripts.Manager.Input.Interaction
{
    public class InputKeyInteraction : MonoBehaviour
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

            var keyboard = UnityEngine.InputSystem.Keyboard.current;

            if (keyboard != null)
                foreach (var control in keyboard.allControls)
                    if (control is KeyControl key && key.wasPressedThisFrame)
                        Debug.Log($"Keyboard Key pressed: {key.name}");
        }

        public InputKeyInteraction Initialize(bool debug)
        {
            this.debug = debug;

            return this;
        }

        public bool IsKeyHeld(string keyName)
        {
            var keyboard = UnityEngine.InputSystem.Keyboard.current;

            if (keyboard == null)
                return false;

            if (!global::System.Enum.TryParse(keyName, true, out UnityEngine.InputSystem.Key key))
                return false;

            return keyboard[key].isPressed;
        }

        public bool IsKeyDown(string keyName)
        {
            var keyboard = UnityEngine.InputSystem.Keyboard.current;

            if (keyboard == null)
                return false;

            if (!global::System.Enum.TryParse(keyName, true, out UnityEngine.InputSystem.Key key))
                return false;

            return keyboard[key].wasPressedThisFrame;
        }

        public bool IsKeyUp(string keyName)
        {
            var keyboard = UnityEngine.InputSystem.Keyboard.current;

            if (keyboard == null)
                return false;

            if (!global::System.Enum.TryParse(keyName, true, out UnityEngine.InputSystem.Key key))
                return false;

            return keyboard[key].wasReleasedThisFrame;
        }
    }
}
