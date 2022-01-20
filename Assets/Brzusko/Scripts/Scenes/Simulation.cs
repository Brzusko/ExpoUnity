using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Simulation : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;
    private static Vector3 _spawnPointPos;
    public static Vector3 Spawn => _spawnPointPos;
    private void Start()
    {
        if(Bootstrapper.Instance.IsServer)
        {
            StartServer();
            _spawnPointPos =_spawnPoint.position;
            return;
        }

        StartClient();
    }
    private void StartServer()
    {
        var netManager = ExpoNetworkManager.Instance;
        netManager.StartServer();
    }

    private void StartClient()
    {
        var netManager = ExpoNetworkManager.Instance;
        netManager.networkAddress = BackendStaticConfig.ServerAddress;
        netManager.StartClient();

        NetworkClient.RegisterHandler<ServerDisableLoading>(DisableLoadingScreen, false);
    }

    private void DisableLoadingScreen(ServerDisableLoading blank)
    {
        var ui = UIReferenceHandler.Instance;
        if(!ui) return;

        ui.LoadingScreen.Active = false;
        NetworkClient.UnregisterHandler<ServerDisableLoading>();
    }
}
