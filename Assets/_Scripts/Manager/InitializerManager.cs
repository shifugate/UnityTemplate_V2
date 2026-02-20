using Assets._Scripts.Manager.Input;
using Assets._Scripts.Manager.Keyboard;
using Assets._Scripts.Manager.Language;
using Assets._Scripts.Manager.Popup;
using Assets._Scripts.Manager.Route;
using Assets._Scripts.Manager.Settings;
using Assets._Scripts.Manager.Sound;
using Assets._Scripts.Manager.System;
using Assets._Scripts.Manager.Timeout;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Scripts.Manager
{
    public class InitializerManagerComplete : UnityEvent { }

    public class InitializerManager : MonoBehaviour
    {
        #region Singleton
        private static bool initialized;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RuntimeInitialize()
        {
            if (initialized)
                return;

            initialized = true;

            Instance.Initialize();
        }

        private static InitializerManager instance;
        public static InitializerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new("InitializerManager");

                    instance = go.AddComponent<InitializerManager>();

                    DontDestroyOnLoad(go);
                }

                return instance;
            }
        }
        #endregion

        private static bool initializeComplete;
        public static bool InitializeComplete { get { return initializeComplete; } }

        public object ScreenManager { get; private set; }

        private void Initialize()
        {
            InstanceNW();
            StartCoroutine(InitializeCR());
        }

        private void InstanceNW()
        {
            SettingsManager.CreateInstance(this);
            InputManager.CreateInstance(this);
            PopupManager.SetInstance(this, Instantiate(Resources.Load<PopupManager>("Manager/Popup/PopupManager")));
            LanguageManager.CreateInstance(this);
            SystemManager.CreateInstance(this);
            TimeoutManager.CreateInstance(this);
            KeyboardManager.SetInstance(this, Instantiate(Resources.Load<KeyboardManager>("Manager/Keyboard/KeyboardManager")));
        }

        private IEnumerator InitializeCR()
        {
            initializeComplete = false;

            yield return RouteManager.CreateInstanceCR(this);
            yield return SoundManager.SetInstanceCR(this, Instantiate(Resources.Load<SoundManager>("Manager/Sound/SoundManager")));

            initializeComplete = true;
        }
    }
}