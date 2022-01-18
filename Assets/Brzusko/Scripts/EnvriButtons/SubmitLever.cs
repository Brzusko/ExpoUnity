using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Brzusko.HTTP;

public class SubmitLever : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SexSwitch3D _sexSwitcher;
    private bool _isActionDone = true;
    public async void Interact()
    {
        if(!_isActionDone) return;
        _isActionDone = false;
        var visualsClient = new VisualsClient();
        var token = PlayerCredentials.Instance.AccessToken;
        var sex = await visualsClient.UpdateSex(token, _sexSwitcher.Value);
        _isActionDone = true;
    }
}
