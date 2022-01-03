using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Brzusko.Events;

public class BackendLiveChecker : SingletonT
{
    public event EventHandler<BackendEventArgs> OnPing; 

    [SerializeField]
    private BackendConfigSO _backendConfigSO;

}
