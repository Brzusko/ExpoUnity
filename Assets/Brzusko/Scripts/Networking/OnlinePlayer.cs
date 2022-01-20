using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Brzusko.HTTP;
using Brzusko.Enums;

public class OnlinePlayer : NetworkBehaviour
{
    [SerializeField]
    private MonoBehaviour[] _scriptsToDisable;

    [SerializeField]
    private GameObject _playerCamera;

    [SerializeField]
    private GameObject[] _playerVis;
    
    [SyncVar(hook = nameof(OnSexChange))]
    private int _sex = 0;
    public override void OnStartClient()
    {
        if(isLocalPlayer)
        {
            CMD_ChangeSex(PlayerCredentials.Instance.AccessToken);
            return;
        }

        DisableScriptForPuppet();
    }

    public override void OnStartServer()
    {
        DisableScriptForPuppet();
    }

    private void DisableScriptForPuppet()
    {
        foreach(var script in _scriptsToDisable)
            script.enabled = false;

            Destroy(_playerCamera);
    }

    private void OnSexChange(int oldSex, int newSex)
    {
        ChangeSex(newSex);
    }

    private void ChangeSex(int newSex)
    {
        foreach(var vis in _playerVis)
            vis.SetActive(false);

        _playerVis[newSex].SetActive(true);
    }

    [Command]
    private async void CMD_ChangeSex(string token)
    {
        if(token == null) return;
        var visClient = new VisualsClient();
        var sex = await visClient.GetSex(token);
        if(sex == null) return;

        _sex = SexMap.SexualMap[sex];
    }
    [Server]
    public void SetSex(int sex) => _sex = sex;
}
