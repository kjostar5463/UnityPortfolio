using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject manager = GameObject.Find(typeof(T).Name);

                if (manager == null)
                {
                    manager = new GameObject(typeof(T).Name);
                    instance = manager.AddComponent<T>();
                }
                else
                {
                    instance = manager.GetComponent<T>();
                }
            }
            return instance;
        }
    }
}
