using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject manager = GameObject.Find(typeof(T).Name);

                if (manager == null)
                {
                    manager = new GameObject(typeof(T).Name);
                    _instance = manager.AddComponent<T>();
                }
                else
                {
                    _instance = manager.GetComponent<T>();
                }
            }
            return _instance;
        }
    }
}
