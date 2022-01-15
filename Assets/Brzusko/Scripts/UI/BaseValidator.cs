using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseValidator : MonoBehaviour, IValidator
{
    [SerializeField]
    protected int _min = 0;
    [SerializeField]
    protected int _max = 0;
    [SerializeField]
    protected TMP_Text _errorLabel;
    protected TMP_InputField _InputField;
    public virtual bool Validate()
    {
        return false;
    }
}
