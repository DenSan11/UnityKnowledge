using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSingleton
{
    public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance = null;

        public static T GetInstance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (FindObjectsOfType<T>().Length > 1)
                {
                    return instance;
                }

                if (instance == null)
                {
                    string instanceName = typeof(T).Name;
                    GameObject instanceGO = GameObject.Find(instanceName);

                    if (instanceGO == null)
                        instanceGO = new GameObject(instanceName);
                    instance = instanceGO.AddComponent<T>();
                    DontDestroyOnLoad(instanceGO);  //保证实例不会被释放
                }
            }

            return instance;
        }

        protected virtual void OnDestroy()
        {
            instance = null;
        }
    }
}