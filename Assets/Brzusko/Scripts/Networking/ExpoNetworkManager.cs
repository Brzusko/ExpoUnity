using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Brzusko.HTTP;
using Brzusko.JSONPayload;
using Brzusko.Enums;
using kcp2k;

public class ExpoNetworkManager : NetworkManager
{
    private class PlayerDetails
    {
        public AccountDetails AccountDetails;
        public int Sex;
    }

    private static ExpoNetworkManager _instance;
    public static ExpoNetworkManager Instance => _instance;
    private Dictionary<int, PlayerDetails> _players;
    
    public override void Start()
    {
        base.Start();

        if(_instance == null)
            _instance = this;
        var transportToEdit = (KcpTransport)transport;
        transportToEdit.Port = (ushort)BackendStaticConfig.ServerPort;
    }

    #region Client
    public override async void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        await Bootstrapper.Instance.LoadScene(SceneType.LoginScene);
    }
    public override async void OnClientConnect()
    {
        var token = PlayerCredentials.Instance.AccessToken;
        if(token == null)
        {
            StopClient();
            await Bootstrapper.Instance.LoadScene(SceneType.LoginScene);
            return;
        }

        NetworkClient.RegisterHandler<AuthRequest>(OnRecivedServerResponse, false);
        NetworkClient.Send<AuthRequest>(new AuthRequest{
            AccessToken = token,
            ResponseStatus = AuthRequest.Status.NONE,
        });
        Debug.Log("Client send request");
    }

    private async void OnRecivedServerResponse(AuthRequest response)
    {
        if(response.ResponseStatus == AuthRequest.Status.REJECT)
        {
            StopClient();
            await Bootstrapper.Instance.LoadScene(SceneType.LoginScene);
            return;
        }
        NetworkClient.Ready();
        NetworkClient.AddPlayer();
    }

    #endregion

    #region Server
    public override void OnStartServer()
    {
        _players = new Dictionary<int, PlayerDetails>();
        NetworkServer.RegisterHandler<AuthRequest>(ServerReciveRequest);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        var spawn = Simulation.Spawn;
        var playerPref = Instantiate(playerPrefab, spawn, Quaternion.identity);
        var player = playerPref.GetComponent<OnlinePlayer>();
        playerPref.name = playerPref.name + $" {conn.connectionId}";

        NetworkServer.AddPlayerForConnection(conn, playerPref);
        conn.Send<ServerDisableLoading>( new ServerDisableLoading());
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        if(!_players.ContainsKey(conn.connectionId)) return;

        _players.Remove(conn.connectionId);
    }
    private async void ServerReciveRequest(NetworkConnection conn, AuthRequest request)
    {
        var accClient = new AccountClient();
        var visClient = new VisualsClient();
        var userData = await accClient.GetAccountInfo(request.AccessToken);
        var sex = await visClient.GetSex(request.AccessToken);
        var response = request;
        response.ResponseStatus = (userData == null || sex == null) ? AuthRequest.Status.REJECT : AuthRequest.Status.OK;
        conn.Send(response);
    }
    #endregion
}
