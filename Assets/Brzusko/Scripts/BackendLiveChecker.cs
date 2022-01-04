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

    private IEnumerator PingCO()
    {
        var links = _backendConfigSO.GetURIs();
        int i = 0;

        while(i < links.Length)
        {
            Debug.Log(links[i]);
            //if (links[i].Length == 0) continue;
            using(var request = UnityWebRequest.Get(links[i]))
            {
                yield return request.SendWebRequest();
                Debug.Log(request.result);
            }
            i++;
        }
    }

    private void Start()
    {
        StartCoroutine(PingCO());
    }

}
