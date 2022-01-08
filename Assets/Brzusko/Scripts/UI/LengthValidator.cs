using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LengthValidator : BaseValidator
{

    private void Start()
    {
        _InputField = GetComponent<TMP_InputField>();
    }

    public override bool Validate()
    {
        var validation = _InputField.text.Length > _min && _InputField.text.Length <= _max;
        _errorLabel.gameObject.SetActive(!validation);
        _errorLabel.text = $"Value should be ${_min}-{_max} length";
        return validation;
    }
}
