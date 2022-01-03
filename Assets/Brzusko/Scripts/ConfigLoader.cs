using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigLoader : MonoBehaviour
{
    [SerializeField]
    private string _backendConfigFileName;

    [SerializeField]
    private string _credentialsFileName;

    private string _cofnigDir;
    private string _credentialsDir;

    private void Awake()
    {
        _cofnigDir = Path.Combine(Application.dataPath, "config");
        _credentialsDir = Path.Combine(Application.dataPath, "credentials");
        Debug.Log(_credentialsDir);
    }

    private void Start()
    {
        CreateFilesIfNotExist();
    }

    private void CreateFilesIfNotExist()
    {
        if (!Directory.Exists(_cofnigDir))
            Directory.CreateDirectory(_cofnigDir);
        if (!Directory.Exists(_credentialsDir))
            Directory.CreateDirectory(_credentialsDir);
    }
}
