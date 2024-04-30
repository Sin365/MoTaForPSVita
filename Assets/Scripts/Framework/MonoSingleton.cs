using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T _instance = null;

    public static T Instance { get => _instance; private set => _instance = value; }

    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}