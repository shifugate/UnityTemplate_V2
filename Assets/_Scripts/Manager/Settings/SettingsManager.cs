using Assets._Scripts.Manager.__Base;
using Assets._Scripts.Manager.Settings.Model;
using Newtonsoft.Json;
using UnityEngine;

#if UNITY_STANDALONE
using System.IO;
#endif

namespace Assets._Scripts.Manager.Settings
{
    public class SettingsManager : BaseManager<SettingsManager>
    {
        private SettingsModel model;
        public SettingsModel Model { get { return model; } }

        protected override void OnInitialize()
        {
            SetProperties();
        }

        private void SetProperties()
        {

#if UNITY_STANDALONE
            model = JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText($"{Application.streamingAssetsPath}/Manager/Settings/settings.json"));
#elif UNITY_ANDROID || UNITY_IOS
            model = JsonConvert.DeserializeObject<SettingsModel>(Resources.Load<TextAsset>("Manager/Settings/settings").text);
#endif
        }
    }
}
