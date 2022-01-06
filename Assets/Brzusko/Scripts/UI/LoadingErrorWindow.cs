using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingErrorWindow : Window
{
    [SerializeField]
    private TMP_Text _errorLabel;
    public void PrintError(string error) => _errorLabel.text = error;
}
