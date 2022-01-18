using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Brzusko.Events;
using Brzusko.HTTP;
using Brzusko.Enums;
using Brzusko.JSONPayload;

public class AboutWindow : Window
{
    private bool _areEventsConnected = false;

    [SerializeField]
    private TMP_Text _loginText;

    [SerializeField]
    private SexSwitch3D _sexSwitcher;

    public override bool Active 
    { 
        get => base.Active; 
        set 
        {
            base.Active = value;

            if(value)
                ConnectEvents();
            else
                DisconnectEvents();
        }
    }

    private void Start()
    {
        UpdatePlayerName("");
    }
    private void OnDestroy()
    {
        DisconnectEvents();
    }

    public async void OnLoginComplete(object sender, BasicMassage message)
    {
        UpdatePlayerName(PlayerCredentials.Instance.PlayerDetails.name);
        var visualClient = new VisualsClient();
        var sex = await visualClient.GetSex(PlayerCredentials.Instance.AccessToken);
        _sexSwitcher.Value = SexMap.SexualMap.ContainsKey(sex) ? SexMap.SexualMap[sex] : 0;
    }

    private void ConnectEvents()
    {
        if(_areEventsConnected) return;
        _areEventsConnected = true;
        PlayerCredentials.Instance.LoginComplete += OnLoginComplete;
    }

    private void DisconnectEvents()
    {
        if(!_areEventsConnected) return;
        _areEventsConnected = false;
        PlayerCredentials.Instance.LoginComplete -= OnLoginComplete;
    }

    public void UpdatePlayerName(string playerName) => _loginText.text = playerName;
}
