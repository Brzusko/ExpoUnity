using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TemporialLabel : MonoBehaviour
{
    private TMP_Text _label;
    private Coroutine _displayCO;

    private void Start() => _label = GetComponent<TMP_Text>();

    private IEnumerator TemporialDisplay(string text, float displayTime)
    {
        _label.text = text;

        yield return new WaitForSeconds(displayTime);

        _label.text = "";
        _displayCO = null;
    }

    public void DisplayText(string text, float displayTime)
    {
        if(_displayCO != null) 
        {
            StopCoroutine(_displayCO);
        }

        _displayCO = StartCoroutine(TemporialDisplay(text, displayTime));
    }
}
