using Assets._Scripts.Manager.__Base;
using Assets._Scripts.Manager.Input.Interaction;
using UnityEngine;

namespace Assets._Scripts.Manager.Input
{
    public class InputManager : BaseManager<InputManager>
    {
        private InputMouseInteraction mouse;
        public InputMouseInteraction Mouse => mouse;

        protected override void OnInitialize()
        {
            mouse = new GameObject("InputMouseInteraction").AddComponent<InputMouseInteraction>();
            mouse.transform.SetParent(transform);
        }
    }
}
