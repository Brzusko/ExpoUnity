using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCredentials : MonoBehaviour
{
    private static PlayerCredentials _instance;
    public static PlayerCredentials Instance {get; private set;}
    private readonly string AUTH_KEY_LOCATION = "Auth-Key";
    private readonly string REFRESH_KEY_LOCATION = "Ref-Key";

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
}
