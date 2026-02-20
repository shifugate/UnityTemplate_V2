using Assets._Scripts.Manager.__Base;
using Assets._Scripts.Manager.Settings;
using Assets._Scripts.Manager.System.Support;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Assets._Scripts.Manager.System
{
    public class SystemManager : BaseManager<SystemManager>
    {
        protected override void OnInitialize()
        {
            SetProperties();
        }

        private void SetProperties()
        {
            if (SettingsManager.Instance.Model.max_fps > 0)
                Application.targetFrameRate = SettingsManager.Instance.Model.max_fps;

            EnhancedTouchSupport.Enable();

            if (SettingsManager.Instance.Model.fps_show)
                gameObject.AddComponent<SystemManagerFPSDisplaySupport>();

            UnityEngine.ResourceManagement.ResourceManager.ExceptionHandler = (op, ex) => throw ex;
        }
    }
}
