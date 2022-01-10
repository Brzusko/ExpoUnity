using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Brzusko.JSONPayload;

public class PlayerCredentials : MonoBehaviour
{
    private static PlayerCredentials _instance;
    public static PlayerCredentials Instance {get; private set;}
    private readonly string AUTH_KEY_LOCATION = "Auth-Key";
    private readonly string REFRESH_KEY_LOCATION = "Ref-Key";

    [Header("Backend Setup")]
    [SerializeField]
    private BackendConfigSO _backendConfig;
    private readonly string[] _headers = {
        "Content-Type",
        "application/json"
    };

    private bool _isActionDone = true;
    public bool IsActionDone
    {
        get => _isActionDone;
    }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
        Instance = this;
    }

    public bool KeysExist()
    {
        return PlayerPrefs.HasKey(AUTH_KEY_LOCATION) && PlayerPrefs.HasKey(REFRESH_KEY_LOCATION);
    }

    public bool IsPlayerLoggedIn()
    {
        return KeysExist();
    }

    public async Task LoginCred(string name, int pinCode)
    {

    }

    public async Task LoginRef()
    {
        await Task.Delay(1);
    }

    public async Task Register(string name)
    {
        // _isActionDone = false;
        // var registerData = new Register { name = name };
        // var uri = $"{_backendConfig.AccountURL}/{_backendConfig.AccountPathsMap[AccountPaths.Register]}";
        // var jsonData = JsonUtility.ToJson(registerData);
        // Debug.Log(uri);
        // using var request = UnityWebRequest.Put(uri, jsonData);
        // request.SetRequestHeader(_headers[0], _headers[1]);
        // var operation = request.SendWebRequest();
        // var result = new RegisterResult();

        // while(!operation.isDone)
        //     await Task.Yield();

        // if(request.result != UnityWebRequest.Result.Success) 
        // {
        //     result.Error = new BackendError{ errorCode = 500 };
        //     Debug.LogError(request.error);
        //     Debug.LogError(request.result);
        //     return result;
        // }

        // if(request.responseCode == 201)
        // {
        //     var error = JsonUtility.FromJson<BackendError>(request.downloadHandler.text);
        //     result.Error = error;
        //     Debug.LogError(request.error);
        //     return result;
        // }

        // var data = JsonUtility.FromJson<LoginCred>(request.downloadHandler.text);
        // result.Data = data;
        // return result;
        await Task.Delay(1);
    }
}
