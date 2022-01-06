using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    protected bool _isActive = false;
    public bool Active
    {
        get => _isActive;
        set
        {
            _isActive = value;
            gameObject.SetActive(value);
        }
    }
}
