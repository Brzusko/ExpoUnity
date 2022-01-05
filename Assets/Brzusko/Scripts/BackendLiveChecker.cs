using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Brzusko.Events;

public class BackendLiveChecker : SingletonT
{
    public event EventHandler<BackendRequestArgs> SentRequest;
    public event EventHandler<BackendResponseArgs> RecivedResponse;
    public event EventHandler<BackendPingArgs> DiscoveryDone;

    [SerializeField]
    private BackendConfigSO _backendConfigSO;

    private readonly ServiceType[] _criticalServices = {
        ServiceType.AuthService,
        ServiceType.AccountService
    };

    private List<ServiceType> _aliveServices = new List<ServiceType>();
    private List<ServiceType> _deadServices = new List<ServiceType>();

    private IEnumerator PingCO()
    {
        var links = _backendConfigSO.GetURIs();
        int i = 0;

        while(i < links.Length)
        {
            using(var request = UnityWebRequest.Get(links[i].Item2))
            {
                yield return request.SendWebRequest();
            }
            i++;
        }
    }

    private void Start()
    {
        StartCoroutine(PingCO());
    }

}
