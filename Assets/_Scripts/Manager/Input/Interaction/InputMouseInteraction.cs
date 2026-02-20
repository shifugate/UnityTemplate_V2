using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._Scripts.Manager.Input.Interaction
{
    public class InputMouseInteraction : MonoBehaviour
    {
        private InputAction pointerAction;

        private bool is0Held;
        private bool is0Down;
        private bool is0Up;

        private void Awake()
        {
            Initialize();
        }

        private void LateUpdate()
        {
            ResetInteraction();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void Dispose()
        {
            pointerAction.Disable();
            pointerAction.Dispose();
        }

        private void ResetInteraction()
        {
            is0Down = false;
            is0Up = false;
        }

        public void Initialize()
        {
            pointerAction = new InputAction(type: InputActionType.Button, binding: "<Pointer>/press");

            pointerAction.performed += ctx =>
            {
                is0Down = true;
                is0Held = true;
            };

            pointerAction.canceled += ctx =>
            {
                is0Up = true;
                is0Held = false;
            };

            pointerAction.Enable();
        }

        public bool GetMouseButton0()
        {
            return is0Held;
        }

        public bool GetMouseButton0Down()
        {
            return is0Down;
        }

        public bool GetMouseButton0Up()
        {
            return is0Up;
        }
    }
}
