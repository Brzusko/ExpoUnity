using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairWIndow : Window
{
    private Color _baseColor;
    [SerializeField]
    private Color _targetColor;

    private Image _crosshairImage;
    private void Start()
    {
        _baseColor = _crosshairImage.color;
        var uiHandler = UIReferenceHandler.Instance;
        if(!uiHandler) return;

        uiHandler.SetCrosshairRef(this);
        Active = false;
    }

    public void UpdateColor(bool isTarget) => _crosshairImage.color = isTarget ? _targetColor : _baseColor;
}
