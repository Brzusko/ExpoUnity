using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonT : MonoBehaviour
{
    protected static SingletonT _instance;
    public static SingletonT Instance;
    
    public T Get<T>() where T : SingletonT
    {
        return (T)_instance;
    }

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
