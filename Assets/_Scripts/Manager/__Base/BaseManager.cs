using System.Collections;
using UnityEngine;

namespace Assets._Scripts.Manager.__Base
{
    public class BaseManager<T> : MonoBehaviour where T : BaseManager<T>
    {
        private static T instance;
        public static T Instance => instance;

        public static void CreateInstance(InitializerManager manager)
        {
            if (instance == null)
            {
                GameObject go = new GameObject(typeof(T).Name);

                instance = go.AddComponent<T>();
                instance.Initialize(manager);
            }
        }

        public static IEnumerator CreateInstanceCR(InitializerManager manager)
        {
            if (instance == null)
            {
                GameObject go = new GameObject(typeof(T).Name);

                instance = go.AddComponent<T>();
            }

            yield return instance.InitializeCR(manager);
        }

        public static void SetInstance(InitializerManager manager, T loadedInstance)
        {
            if (instance == null)
            {
                instance = loadedInstance;
				instance.name = typeof(T).Name;
                instance.Initialize(manager);
            }
        }

        public static IEnumerator SetInstanceCR(InitializerManager manager, T loadedInstance)
        {
            if (instance == null)
                instance = loadedInstance;

            yield return instance.InitializeCR(manager);
        }

        private void Initialize(InitializerManager manager)
        {
            transform.SetParent(manager.transform);

            OnInitialize();
        }

        protected virtual IEnumerator InitializeCR(InitializerManager manager)
        {
            transform.SetParent(manager.transform);

            yield return OnInitializeCR();
        }

        protected virtual IEnumerator OnInitializeCR()
        {
            yield return null;
        }

        protected virtual void OnInitialize() { }
    }
}
