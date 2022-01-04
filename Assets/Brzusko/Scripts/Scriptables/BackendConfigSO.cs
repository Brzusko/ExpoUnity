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

    public string[] GetURIs()
    {
        return new string[]
        {
            AuthURL,
            AccountURL,
            VisualsURL,
            PositionURL
        };
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
