using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ConfigLoader : MonoBehaviour
{
    [SerializeField]
    private string _backendConfigFileName;

    [SerializeField]
    private BackendConfigSO _backendConfig;

    private string _cofnigDir;

    private void Awake()
    {
        _cofnigDir = Path.Combine(@Application.dataPath, "config");
    }

    private void Start()
    {
        CreateFilesIfNotExist();
        LoadConfig();
    }

    private void CreateFilesIfNotExist()
    {
        if (!Directory.Exists(_cofnigDir))
            Directory.CreateDirectory(_cofnigDir);

        var configPath = Path.Combine(_cofnigDir, _backendConfigFileName);
  
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
        var configPath = Path.Combine(_cofnigDir, _backendConfigFileName);
        using(var fs = File.OpenRead(configPath))
        using(var reader = new StreamReader(fs))
        {
            fileData = JsonUtility.FromJson<BackendConfigFileData>(reader.ReadToEnd());
        }
        _backendConfig.AccountURL = fileData.AccountURL;
        _backendConfig.AuthURL = fileData.AuthURL;
        _backendConfig.PositionURL = fileData.PositionURL;
        _backendConfig.VisualsURL = fileData.VisualsURL;
    }
}
