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

public class Bootstrapper : MonoBehaviour
{
    [SerializeField]
    private AssetReference[] _crucialScenes;
    [SerializeField]
    private AssetReference _loginScene;

    [SerializeField]
    private Camera _bootstrapCamera;
    private BackendLiveChecker _backendLiveChecker;
    private LoadingScreen _loadingScreen;
    private PlayerCredentials _playerCredentials;
    private PlayerInput _inputs;
    private bool _applicationExitIsActive = false;
    private SceneInstance _offlineScene;

    private async void Start()
    {
        await LoadBasicScenes();
        GetReferences();
        EnableInputs();
        ConnectEvents();
        _backendLiveChecker.StartPinging(); 
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
            _loadingScreen.ShowErrorMessage("Critical services are dead!");
            _applicationExitIsActive = true;
            return;
        }

        // if(!_playerCredentials.KeysExist())
        // {
        //     await ChangeToLoginScreen();
        //     return;
        // }
        // Login with Refresh token

        // await for login
    }

    private async Task ChangeToLoginScreen()
    {
        _loadingScreen.ChangeLoadingInfo("Loading login scene");
        DestroyImmediate(_bootstrapCamera.gameObject);
        var _offlineScene = await _loginScene.LoadSceneAsync(LoadSceneMode.Additive, true).Task;
        await Task.Delay(5 * 1000);
        _loadingScreen.Active = false;
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
