using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour, IInteractable
{
    public enum InteractionType
    {
        LEFT,
        RIGHT
    }

    [SerializeField]
    private Switch3D _parent;

    [SerializeField]
    private InteractionType _intraction;
    private Action _interactionCallback;
    private void Start() => _interactionCallback = _intraction == InteractionType.LEFT ? LeftInteraction : RightInteraction;
    public void LeftInteraction() => _parent.Decrement();
    public void RightInteraction() => _parent.Increment();
    public void Interact() => _interactionCallback?.Invoke();
}
