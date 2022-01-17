using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Brzusko.Events;

public class AboutWindow : Window
{
    private bool _areEventsConnected = false;

    [SerializeField]
    private TMP_Text _loginText;

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

    public void OnLoginComplete(object sender, BasicMassage message)
    {
        UpdatePlayerName(PlayerCredentials.Instance.PlayerDetails.name);   
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
