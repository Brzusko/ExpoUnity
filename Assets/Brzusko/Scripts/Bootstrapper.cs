using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using Brzusko.Events;

public enum SceneType
{
    LoginScene = 0,
    Simulation = 1,
}

public class SceneWrapper
{
    public Scene Scene;
}

public class Bootstrapper : MonoBehaviour
{
    [SerializeField]
    private AssetReference[] _crucialScenes;
    [SerializeField]
    private AssetReference[] _scenes;
    [SerializeField]
    private Camera _bootstrapCamera;
    private BackendLiveChecker _backendLiveChecker;
    private LoadingScreen _loadingScreen;
    private PlayerCredentials _playerCredentials;
    private PlayerInput _inputs;
    private bool _applicationExitIsActive = false;
    private SceneWrapper _currentScene = null;
    private static Bootstrapper _instance;
    public Bootstrapper Instance => _instance;

    private async void Start()
    {
        await LoadBasicScenes();
        GetReferences();
        EnableInputs();
        ConnectEvents();
        _backendLiveChecker.StartPinging(); 
        _instance = this;
    }

    private void OnDestroy()
    {
        DisconnectEvents();
    }

    private void OnPingSent(object sender, BackendRequestArgs arg)
    {
        _loadingScreen.ChangeLoadingInfo($"Checking live status of service: {arg.ServiceType.ToString()}");
    }

    private void OnPongRecived(object sender, BackendResponseArgs arg)
    {

    }

    private async void OnDiscoveryDone(object sender, BackendPingArgs arg)
    {
        if(arg.CriticalServicesAreDead)
        {
            var text = "Critical services are dead!";

            _loadingScreen.ShowErrorMessage(text);
            _applicationExitIsActive = true;
            return;
        }

        DisconnectEvents();
        await LoadScene(SceneType.LoginScene);
        DestroyImmediate(_bootstrapCamera.gameObject);
    }

    public async Task LoadScene(SceneType sceneToLoad)
    {
        if((int)sceneToLoad >= _scenes.Length) return;
        _loadingScreen.Active = true;
        _loadingScreen.ChangeLoadingInfo($"Loading: {sceneToLoad.ToString()}");
        await Task.Delay(3 * 1000);
        var sceneRef = _scenes[(int)sceneToLoad]; 
        var sceneInstance = await sceneRef.LoadSceneAsync(LoadSceneMode.Additive, true).Task;

        if(_currentScene != null)
        {
            var operation = SceneManager.UnloadSceneAsync(_currentScene.Scene);
            while(!operation.isDone)
                await Task.Yield();
        }

        _currentScene = new SceneWrapper{ Scene = sceneInstance.Scene };
    }

    private void OnQuitPressed(InputAction.CallbackContext ctx)
    {
        if(!_applicationExitIsActive) return;
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void ConnectEvents()
    {
        _backendLiveChecker.SentRequest += OnPingSent;
        _backendLiveChecker.RecivedResponse += OnPongRecived;
        _backendLiveChecker.DiscoveryDone += OnDiscoveryDone;
        _inputs.actions["Quit"].performed += OnQuitPressed;
    }

    private void DisconnectEvents()
    {
        _backendLiveChecker.SentRequest -= OnPingSent;
        _backendLiveChecker.RecivedResponse -= OnPongRecived;
        _backendLiveChecker.DiscoveryDone -= OnDiscoveryDone;
        _inputs.actions["Quit"].performed -= OnQuitPressed;
    }

    private void GetReferences()
    {
        _backendLiveChecker = BackendLiveChecker.Instance;
        _loadingScreen = UIReferenceHandler.Instance.LoadingScreen;
        _playerCredentials = PlayerCredentials.Instance;
        _inputs = GetComponent<PlayerInput>();
    }

    private void EnableInputs() => _inputs.ActivateInput();
    private void DisableInputs() => _inputs.DeactivateInput();
    private async Task LoadBasicScenes()
    {
        // Saving for example
        // var loadingScenes = Enumerable.Range(0, _crucialScenes.Length).Select(_ => _crucialScenes[_].LoadSceneAsync(LoadSceneMode.Additive).Task);
        // var result = await Task.WhenAll(loadingScenes);
        foreach(var sceneToLoad in _crucialScenes)
        {
            await sceneToLoad.LoadSceneAsync(LoadSceneMode.Additive).Task;
        }
    }
}
