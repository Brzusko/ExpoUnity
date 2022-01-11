using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Brzusko.Events;
using Brzusko.HTTP;

public class PlayerCredentials : MonoBehaviour
{
    public event EventHandler<BasicMassage> RegisterComplete;
    public event EventHandler<BasicMassage> RegisterFailed;
    public event EventHandler<BasicMassage> LoginComplete;
    public event EventHandler<BasicMassage> LoginFailed;
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
        if(!_isActionDone) return;
        _isActionDone = false;

        var httpClient = new LoginClient(_backendConfig);
        var result = await httpClient.LoginCred(name, pinCode);

        if(result == null)
            LoginFailed.Invoke(this, new BasicMassage{ Message = "Username or pincode is not valid!" });
        else
            LoginComplete.Invoke(this, new BasicMassage{ Message = "Logged in!" });

        _isActionDone = true;
    }

    public async Task LoginRef()
    {
        await Task.Delay(1);
    }

    public async Task Register(string name)
    {
        if(!_isActionDone) return;
        _isActionDone = false;
        var httpClient = new AccountClient(_backendConfig);
        var result = await httpClient.CreateAccount(name);

        if(result == null)
            RegisterFailed?.Invoke(this, new BasicMassage{ Message = "Register Failed!" });
        else
            RegisterComplete?.Invoke(this, new BasicMassage { Message = $"You can login now with username: {result.credentials.name} and pin code: [{result.credentials.pinCode}]" });

        _isActionDone = true;
    }
}
