using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InsertPrefixToLabel : MonoBehaviour
{
    private TMP_Text _text;

    [Header("Text properties")]
    [SerializeField]
    private string _prefix = "";
    
    [SerializeField]
    private string _sufix = "";

    protected virtual void Start() => _text = GetComponent<TMP_Text>();
    public void ChangeText(string text) => _text.text = $"{_prefix}{text}{_sufix}";
    public void Hide() => gameObject.SetActive(false);
    public void Show() => gameObject.SetActive(true);
}
