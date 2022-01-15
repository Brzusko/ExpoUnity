using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinCodeValidator : BaseValidator
{
    private void Start()
    {
        _InputField = GetComponent<TMP_InputField>();
    }
    public override bool Validate()
    {
        var validation = _InputField.text.Length == _min;
        _errorLabel.gameObject.SetActive(!validation);
        _errorLabel.text = $"Value legnth have to be equal (${_min})";
        return validation;
    }
}
