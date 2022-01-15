using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Brzusko.Events;
using Brzusko.HTTP;
using Brzusko.JSONPayload;

public class PlayerCredentials : MonoBehaviour
{
    public event EventHandler<BasicMassage> RegisterComplete;
    public event EventHandler<BasicMassage> RegisterFailed;
    public event EventHandler<BasicMassage> LoginComplete;
    public event EventHandler<BasicMassage> LoginFailed;
    public event EventHandler<BasicMassage> LogoutComplete;

    private static PlayerCredentials _instance;
    public static PlayerCredentials Instance {get; private set;}
    private readonly string AUTH_KEY_LOCATION = "Auth-Key";
    private readonly string REFRESH_KEY_LOCATION = "Ref-Key";

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

    public async Task LoginCred(string name, int pinCode)
    {
        if(!_isActionDone) return;
        _isActionDone = false;

        var httpClient = new LoginClient();
        var result = await httpClient.LoginCred(name, pinCode);

        Debug.Log(result.refreshToken);
        Debug.Log(result.authToken);

        PropagateLoginEvents(result);
        SaveKeys(result);

        _isActionDone = true;
    }

    public async Task LoginRef()
    {
        if(!_isActionDone) return;
        _isActionDone = false;

        if(!KeysExist())
        {
            LoginFailed?.Invoke(this, new BasicMassage { Message = "Missing refresh token!" });
            return;
        }

        var key = PlayerPrefs.GetString(REFRESH_KEY_LOCATION);
        var httpClient = new LoginClient();
        var result = await httpClient.LoginRef(key);

        PropagateLoginEvents(result);
        SaveKeys(result);

        _isActionDone = true;
    }

    private void PropagateLoginEvents(Credentials result)
    {
        if(result == null)
            LoginFailed.Invoke(this, new BasicMassage{ Message = "Username or pincode is not valid!" });
        else
            LoginComplete.Invoke(this, new BasicMassage{ Message = "Logged in!" });
    }

    private void SaveKeys(Credentials credentials)
    {
        if(credentials == null) return;
        
        if(credentials.authToken != null)
            PlayerPrefs.SetString(AUTH_KEY_LOCATION, credentials.authToken);
        
        if(credentials.refreshToken != null)
            PlayerPrefs.SetString(REFRESH_KEY_LOCATION, credentials.refreshToken);
            
        PlayerPrefs.Save();
    }

    private void FlushKeys()
    {
        PlayerPrefs.DeleteKey(AUTH_KEY_LOCATION);
        PlayerPrefs.DeleteKey(REFRESH_KEY_LOCATION);
        PlayerPrefs.Save();
    }

    public async Task Register(string name)
    {
        if(!_isActionDone) return;
        _isActionDone = false;
        var httpClient = new AccountClient();
        var result = await httpClient.CreateAccount(name);

        if(result == null)
            RegisterFailed?.Invoke(this, new BasicMassage{ Message = "Register Failed!" });
        else
            RegisterComplete?.Invoke(this, new BasicMassage { Message = $"You can login now with username: {result.credentials.name} and pin code: [{result.credentials.pinCode}]" });

        _isActionDone = true;
    }

    public async Task Logout()
    {
        if(!_isActionDone) return;
        _isActionDone = false;

        var key = PlayerPrefs.GetString(REFRESH_KEY_LOCATION);
        var httpClient = new LoginClient();
        var result = await httpClient.Logout(key);

        _isActionDone = true;
        
        if(result == null || result.message == null || result.message.Length == 0)
            return;
        
        FlushKeys();
        LogoutComplete?.Invoke(this, new BasicMassage { Message = result.message });
    }
}
