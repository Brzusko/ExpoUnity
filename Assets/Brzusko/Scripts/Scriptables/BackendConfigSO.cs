using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackendConfig", menuName = "Configs/BackendConfig")]
public class BackendConfigSO : ScriptableObject
{
    public string AuthURL;
    public string AccountURL;
    public string VisualsURL;
    public string PositionURL;

    public Stack<Tuple<ServiceType, string>> GetURIs()
    {
        return new Stack<Tuple<ServiceType, string>>
        (new[] {
            new Tuple<ServiceType, string>(ServiceType.AuthService, AuthURL),
            new Tuple<ServiceType, string>(ServiceType.AccountService, AccountURL),
            new Tuple<ServiceType, string>(ServiceType.VisualsService, VisualsURL),
            new Tuple<ServiceType, string>(ServiceType.PositionService, PositionURL)
        });
    }
}

[Serializable]
public class BackendConfigFileData
{
    public string AuthURL;
    public string AccountURL;
    public string VisualsURL;
    public string PositionURL;
}

public enum ServiceType
{
    AuthService,
    AccountService,
    VisualsService,
    PositionService,      
}