using Assets._Scripts.Manager.__Base;
using Assets._Scripts.Manager.Input.Interaction;
using Assets._Scripts.Util;
using System.IO;
using UnityEngine;

namespace Assets._Scripts.Manager.Input
{
    public class InputManager : BaseManager<InputManager>
    {
        private InputMouseInteraction mouse;
        public InputMouseInteraction Mouse => mouse;

        private InputKeyInteraction key;
        public InputKeyInteraction Key => key;

        private InputJoystickInteraction joystick;
        public InputJoystickInteraction Joystick => joystick;

        private InputGamepadInteraction gamepad;
        public InputGamepadInteraction Gamepad => gamepad;

        protected override void OnInitialize()
        {
            var debug = File.Exists($"{CommonUtil.DataPath}/debug_input.txt");

            mouse = new GameObject("InputMouseInteraction").AddComponent<InputMouseInteraction>().Initialize(debug);
            mouse.transform.SetParent(transform);

            key = new GameObject("InputKeyInteraction").AddComponent<InputKeyInteraction>().Initialize(debug);
            key.transform.SetParent(transform);

            joystick = new GameObject("InputJoystickInteraction").AddComponent<InputJoystickInteraction>().Initialize(debug);
            joystick.transform.SetParent(transform);

            gamepad = new GameObject("InputGamepadInteraction").AddComponent<InputGamepadInteraction>().Initialize(debug);
            gamepad.transform.SetParent(transform);
        }
    }
}
