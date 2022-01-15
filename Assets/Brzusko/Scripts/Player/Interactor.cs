using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private int _interactionLayer = 0;
    [SerializeField]
    private float _rayLength;
    private int _bitMask;
    private CrosshairWIndow _crosshair;
    private InputSampler _input;
    private IInteractable _toInteract;
    public IInteractable Interactable => _toInteract;

    private void Start()
    {
        _bitMask = 1 << _interactionLayer;
        _input = GetComponent<InputSampler>();
        _crosshair = UIReferenceHandler.Instance.Crosshair;
        _input.TriggerAction += (object sender, EventArgs arg) => _toInteract?.Interact();
    }

    private void OnDestroy()
    {
        _input.TriggerAction -= (object sender, EventArgs arg) => _toInteract?.Interact();
    }

    private void FixedUpdate() => PreformRaycast();

    private void PreformRaycast()
    {
        _toInteract = null;
        var ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if(!Physics.Raycast(ray, out hit, _rayLength, _bitMask))
        {
            _crosshair.UpdateColor(false);
            return;
        }

        var interactable = hit.collider.GetComponent<IInteractable>();
        _toInteract = interactable;

        _crosshair.UpdateColor(true);
    }
}
