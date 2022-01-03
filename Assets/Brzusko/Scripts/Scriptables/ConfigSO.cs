using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackendConfig", menuName = "Configs/BackendConfig")]
[Serializable]
public class ConfigSO : ScriptableObject
{
    public string AuthURL;
    public string AccountURL;
    public string VisualsURL;
    public string PositionURL;
}
