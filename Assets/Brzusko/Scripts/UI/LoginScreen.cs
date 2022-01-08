using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    private void Start()
    {
        _loginSwitcher.Show();
        var uiHandler = UIReferenceHandler.Instance;

        if(!uiHandler) return;
        uiHandler.SetLoginScreenRef(this);
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
        await PlayerCredentials.Instance.Login();
    }

    public async void SubmitRegister()
    {
        if(!PlayerCredentials.Instance.IsActionDone || !ValidateRegister()) return;
        await PlayerCredentials.Instance.Login();
    }

}
