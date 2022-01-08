using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewSwitcher : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField]
    private float _deactivatedTextAlpha;
    [SerializeField]
    private float _activeTextAlpha;

    [Header("References")]
    [SerializeField]
    private ViewSwitcher _toDeactive;
    [SerializeField]
    private CanvasGroup _toShow;
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private Image _border;

    public void OnClick()
    {
        Show();
    }

    public void Show()
    {
        _toShow.alpha = 1.0f;
        _toShow.gameObject.SetActive(true);
        _toDeactive.Hide();
        SetOnActiveVisuals();
    }

    public void Hide()
    {
        _toShow.alpha = 0.0f;
        _toShow.gameObject.SetActive(false);
        SetOnDeactivatedVisuals();
    }

    private void SetOnActiveVisuals()
    {
        _text.alpha = _activeTextAlpha;
        var borderColor = _border.color;
        borderColor.a = 1.0f;
        _border.color = borderColor;
    }

    private void SetOnDeactivatedVisuals()
    {
        _text.alpha = _deactivatedTextAlpha;
        var borderColor = _border.color;
        borderColor.a = 0.0f;
        _border.color = borderColor;
    }

}
