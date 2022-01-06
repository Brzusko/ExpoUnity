using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReferenceHandler : MonoBehaviour
{
    private static UIReferenceHandler _instance;
    public static UIReferenceHandler Instance{ get; private set; }
    public LoadingScreen LoadingScreen { get => _loadingScreen; }
    private LoadingScreen _loadingScreen;

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
    public void SetLoadingScreenRef(LoadingScreen loadingScreen) => _loadingScreen = loadingScreen;
}
