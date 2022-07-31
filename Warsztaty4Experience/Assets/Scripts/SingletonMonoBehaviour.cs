using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; set; }
    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = this as T;
    }
}
