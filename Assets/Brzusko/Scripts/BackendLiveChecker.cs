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
        ServiceType.AccountService,
        ServiceType.PositionService
    };

    private List<ServiceType> _aliveServices = new List<ServiceType>();
    private List<ServiceType> _deadServices = new List<ServiceType>();

    private Coroutine _pingInFly;
    private IEnumerator PingCO()
    {
        var links = _backendConfigSO.GetURIs();

        while(links.Count != 0)
        {
            var link = links.Pop();
            var serviceType = link.Item1;
            var uri = link.Item2;

            using(var request = UnityWebRequest.Get(uri))
            {
                SentRequest?.Invoke(this, new BackendRequestArgs{ ServiceType = serviceType });

                yield return request.SendWebRequest();

                if(request.result != UnityWebRequest.Result.Success)
                    _deadServices.Add(serviceType);
                else
                    _aliveServices.Add(serviceType);

                RecivedResponse?.Invoke(this, new BackendResponseArgs
                {
                    ServiceType = serviceType,
                    Status = request.responseCode,
                    ResponseMessage = request.result.ToString()
                });
            }
        }

        DiscoveryDone?.Invoke(this, new BackendPingArgs
        {
            SuccessPings = _aliveServices.ToArray(),
            FailedPings = _deadServices.ToArray(),
            CriticalServicesAreDead = AreCriticialServicesDead()
        });

        _pingInFly = null;
    }

    private bool AreCriticialServicesDead()
    {
        foreach(var service in _criticalServices)
        {
            var exist = _deadServices.Exists(deadService => deadService == service);
            if(exist) return true;
        }
        return false;
    }

    public void StartPinging()
    {
        if(_pingInFly != null) return;
        _pingInFly = StartCoroutine(PingCO());
    }
}
