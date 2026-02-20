using Assets._Scripts.Manager.__Base;
using Assets._Scripts.Manager.Settings;
using Assets._Scripts.Manager.Timeout.Event;
using UnityEngine;

namespace Assets._Scripts.Manager.Timeout
{
    public class TimeoutManager : BaseManager<TimeoutManager>
    {
        private float time;

        private void Update()
        {
            VerifyInput();
            UpdateTime();
        }

        protected override void OnInitialize()
        {
            SetProperties();
        }

        private void SetProperties()
        {
            ResetTime();
        }

        private void VerifyInput()
        {
           if ((UnityEngine.InputSystem.Keyboard.current?.anyKey != null && UnityEngine.InputSystem.Keyboard.current.anyKey.wasPressedThisFrame) 
			|| (UnityEngine.InputSystem.Mouse.current?.leftButton != null && UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
			|| UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0)
			time = 0;
        }

        private void UpdateTime()
        {
            time += Time.deltaTime;

            if (time > SettingsManager.Instance.Model.timeout)
            {
                ResetTime();

                TimeoutManagerEvent.OnTimeout?.Invoke();
            }
        }

        public void ResetTime()
        {
            time = 0;
        }
    }
}
