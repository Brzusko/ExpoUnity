using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonT : MonoBehaviour
{
    protected static SingletonT _instance;
    protected virtual void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
    }
}
