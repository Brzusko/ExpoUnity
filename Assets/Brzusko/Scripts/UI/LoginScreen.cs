using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Brzusko.Events;

public class LoginScreen : Window
{
    [SerializeField]
    private ViewSwitcher _loginSwitcher;
    [SerializeField]
    private ViewSwitcher _registerSwitcher;
    [SerializeField]
    private BaseValidator[] _loginFields;
    [SerializeField]
    private BaseValidator _registerField;
    [SerializeField]
    private TMP_InputField _registerInput;
    [SerializeField]
    private TMP_InputField _loginInput;
    [SerializeField]
    private TMP_InputField _pinCodeInput;

    [SerializeField]
    private TemporialLabel _tempLabel;

    public override bool Active 
    {
        get
        {
            return base.Active;
        } 
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
        _loginSwitcher.Show();
        var uiHandler = UIReferenceHandler.Instance;

        if(!uiHandler) return;
        uiHandler.SetLoginScreenRef(this);
        gameObject.SetActive(false);
    }

    private bool ValidateLogin() 
    {
        var validationList = new List<bool>();
        foreach(var validator in _loginFields)
            validationList.Add(validator.Validate());
        return !validationList.Exists(match => match == false);
    }

    private bool ValidateRegister() => _registerField.Validate();
    
    public async void SubmitLogin()
    {
        if(!PlayerCredentials.Instance.IsActionDone || !ValidateLogin()) return;
        await PlayerCredentials.Instance.LoginCred(_loginInput.text, Int32.Parse(_pinCodeInput.text));
    }

    public async void SubmitRegister()
    {
        if(!PlayerCredentials.Instance.IsActionDone || !ValidateRegister()) return;
        await PlayerCredentials.Instance.Register(_registerInput.text);
    }

    protected virtual void DisplayMassage(object sender, BasicMassage message) => _tempLabel.DisplayText(message.Message, 20f);
    protected virtual void LoginSuccess(object sender, BasicMassage message)
    {
        // TODO
        Active = false;
    }

    private void ConnectEvents()
    {
        var playerCred = PlayerCredentials.Instance;
        playerCred.RegisterComplete += DisplayMassage;
        playerCred.RegisterFailed += DisplayMassage;
        playerCred.LoginFailed += DisplayMassage;
        playerCred.LoginComplete += LoginSuccess;
    }

    private void DisconnectEvents()
    {
        var playerCred = PlayerCredentials.Instance;
        playerCred.RegisterComplete -= DisplayMassage;
        playerCred.RegisterFailed -= DisplayMassage;
        playerCred.LoginFailed -= DisplayMassage;
        playerCred.LoginComplete -= LoginSuccess;
    }
}
