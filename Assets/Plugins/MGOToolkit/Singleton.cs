using System;
using UnityEngine;

namespace MGO
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static readonly Lazy<T> LazyInstance = new Lazy<T>(CreateSingleton);

        public static T Instance => LazyInstance.Value;

        protected virtual void Awake()
        {
            if (LazyInstance.IsValueCreated)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Debug.Log("Singleton Init :" + Instance);
            }
        }

        private static T CreateSingleton()
        {

            var instance = FindObjectOfType<T>();
            if (instance == null)
            {
                var ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
                instance = ownerObject.AddComponent<T>();
            }
            DontDestroyOnLoad(instance.gameObject);
            return instance; 
        }
    }

    public abstract class SingletonInScene<T> : MonoBehaviour where T : SingletonInScene<T>
    {
        private static T instance = null;
        public static T Instance { get {
                if (instance == null)
                {
                    CreateSingleton();
                }
                return instance;
            } 
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private static T CreateSingleton()
        {
            instance = FindObjectOfType<T>();
            if (instance == null)
            {
                var ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
                instance = ownerObject.AddComponent<T>();
            }
            return instance;
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}