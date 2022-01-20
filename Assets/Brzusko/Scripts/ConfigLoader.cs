using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public static class BackendStaticConfig
{
    public static string AuthURL;
    public static string AccountURL;
    public static string VisualsURL;
    public static string PositionURL;
    public static string ServerAddress;
    public static int ServerPort;

    public static Stack<Tuple<ServiceType, string>> GetURIs()
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

public class ConfigLoader : MonoBehaviour
{
    [SerializeField]
    private string _backendConfigFileName;

    [SerializeField]
    private BackendConfigSO _backendConfig;

    //private string _cofnigDir;
    public static string _configDir;

    private void Awake()
    {
        _configDir = Application.dataPath + "/config";
    }

    private void Start()
    {
        CreateFilesIfNotExist();
        LoadConfig();
    }

    private void CreateFilesIfNotExist()
    {
        if (!Directory.Exists(_configDir))
            Directory.CreateDirectory(_configDir);

        var configPath = Path.Combine(_configDir, _backendConfigFileName);
  
        if (!File.Exists(configPath))
        {
            var configData = JsonUtility.ToJson(new BackendConfigFileData());
            
            using(var fileStream = File.Create(configPath))
            using (var writer = new StreamWriter(fileStream))
            {
                writer.Write(configData);
            }
        }
    }

    private void LoadConfig()
    {
        BackendConfigFileData fileData;
        var configPath = Path.Combine(_configDir, _backendConfigFileName);
        using(var fs = File.OpenRead(configPath))
        using(var reader = new StreamReader(fs))
        {
            fileData = JsonUtility.FromJson<BackendConfigFileData>(reader.ReadToEnd());
        }
        BackendStaticConfig.AccountURL = fileData.AccountURL;
        BackendStaticConfig.AuthURL = fileData.AuthURL;
        BackendStaticConfig.PositionURL = fileData.PositionURL;
        BackendStaticConfig.VisualsURL = fileData.VisualsURL;
        BackendStaticConfig.ServerAddress = fileData.ServerAddress;
        BackendStaticConfig.ServerPort = fileData.ServerPort;
    }
}
