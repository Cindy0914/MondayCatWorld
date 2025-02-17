using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance)
                return instance;
            else
                return null;
        }
    }

    public void Awake()
    {
        instance = this as T;
    }
    
    public void OnDestroy()
    {
        instance = null;
    }
}
