using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputSampler : MonoBehaviour
{
    public event EventHandler TriggerAction;
    [SerializeField]
    private PlayerInput _playerInput;
    private bool _isActionPressed;
    private InputSample _inputSample;

    public InputSample CurrentImputSample
    {
        get => _inputSample;
    }

    private void Start()
    {
        _playerInput.ActivateInput();
        _playerInput.actions["Use"].performed += ctx => TriggerAction?.Invoke(this, null);
    }

    private void OnDisable()
    {
        _playerInput.DeactivateInput();
        _playerInput.actions["Use"].performed -= ctx => TriggerAction?.Invoke(this, null);
    }
    private void Update() => SampleImput();

    private void SampleImput()
    {
        _inputSample = new InputSample
        {
            Movement = _playerInput.actions["Move"].ReadValue<Vector2>(),
            MouseDelta = _playerInput.actions["Look"].ReadValue<Vector2>()
        };
    }

}

[Serializable]
public class InputSample
{
    public Vector2 Movement;
    public Vector2 MouseDelta;
}
