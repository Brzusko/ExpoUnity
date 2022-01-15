using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputSampler : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;
    private bool _isActionPressed;
    private InputSample _inputSample;

    public InputSample CurrentImputSample
    {
        get => _inputSample;
    }

    private void Start() => _playerInput.ActivateInput();

    private void OnDisable() => _playerInput.DeactivateInput();

    private void Update() => SampleImput();

    private void SampleImput()
    {
        _inputSample = new InputSample
        {
            Movement = _playerInput.actions["Move"].ReadValue<Vector2>(),
            TriggerAction = _playerInput.actions["Use"].ReadValue<bool>(),
            MouseDelta = _playerInput.actions["Look"].ReadValue<Vector2>()
        };
    }

}

[Serializable]
public class InputSample
{
    public Vector2 Movement;
    public bool TriggerAction;
    public Vector2 MouseDelta;
}
