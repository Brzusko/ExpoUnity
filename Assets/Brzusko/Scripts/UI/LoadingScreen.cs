using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : Window
{
    [SerializeField]
    private InsertPrefixToLabel _loadingInfoLabel;
    [SerializeField]
    private LoadingErrorWindow _errorWindow;
    [SerializeField]
    private GameObject[] _hideOnError;
    private Coroutine _textHideCO;

    private void Start()
    {
        Debug.Log("Loaded HUDs");
        var uiRef = UIReferenceHandler.Instance;
        if(!uiRef) return;
        uiRef.SetLoadingScreenRef(this);
    }

    public void ShowErrorMessage(string message)
    {
        foreach(var toHide in _hideOnError)
            toHide.SetActive(false);
        
        _errorWindow.Active = true;
        _errorWindow.PrintError(message);
    }

    // lastTime = 0 its infinity message
    private IEnumerator LoadingLabelClear(float lastTime)
    {
        yield return new WaitForSeconds(lastTime);
        _loadingInfoLabel.ChangeText("");
    }

    public void ChangeLoadingInfo(string message, float lastTime = 0)
    {
        if(_textHideCO != null)
        {
            StopCoroutine(_textHideCO);
            _textHideCO = null;
        }

        if(lastTime > 0)
        {
            _textHideCO = StartCoroutine(LoadingLabelClear(lastTime));
        }

        _loadingInfoLabel.ChangeText(message);
    }
}
