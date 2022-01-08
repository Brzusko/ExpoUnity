using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerCredentials : MonoBehaviour
{
    private static PlayerCredentials _instance;
    public static PlayerCredentials Instance {get; private set;}
    private readonly string AUTH_KEY_LOCATION = "Auth-Key";
    private readonly string REFRESH_KEY_LOCATION = "Ref-Key";
    private bool _isActionDone = true;
    public bool IsActionDone
    {
        get => _isActionDone;
    }

    [SerializeField]
    private BackendConfigSO _backendConfig;

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

    public async Task Login()
    {
        Debug.Log("Login Request");
        _isActionDone = false;
        await Task.Delay(5000);
        _isActionDone = true;
        Debug.Log("Login Done");
    }

    public async Task Register()
    {
        _isActionDone = false;
        await Task.Delay(1500);
        _isActionDone = true;
    }
}
