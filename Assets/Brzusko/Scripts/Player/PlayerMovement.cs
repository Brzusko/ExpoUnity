using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private InputSampler _inputSampler;
    [SerializeField]
    private Rigidbody _rg;
    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField]
    private Transform _head;

    [SerializeField]
    private float _maxHeadRot = 62.32f;
    [SerializeField]
    private float _minHeadRot = -62.32f;

    [SerializeField]
    private float _rotationSpeed = 1f;
    private Vector3 _velocity;
    private Vector3 _desiredVelicity;
    private float _currentYCameraRotation = 0.0f;

    private void FixedUpdate() 
    {
        if(_inputSampler.CurrentImputSample == null) return;
        PreformRotation();
        PreformMovement();
    }

    private void PreformRotation()
    {
        var mouseDelta = _inputSampler.CurrentImputSample.MouseDelta;
        var oldEulerAngles = transform.eulerAngles;
        var eulerAngles = new Vector3(oldEulerAngles.x, oldEulerAngles.y + (mouseDelta.x * _rotationSpeed * Time.fixedDeltaTime), oldEulerAngles.z);
        transform.rotation = Quaternion.Euler(eulerAngles);

        var xAngle = (-mouseDelta.y * _rotationSpeed * Time.fixedDeltaTime);
        _currentYCameraRotation = this.Clamp(_currentYCameraRotation + xAngle ,_minHeadRot, _maxHeadRot);
        _head.localEulerAngles = new Vector3(_currentYCameraRotation, 0, 0);
    }
    private void PreformMovement()
    {
        var movement = _inputSampler.CurrentImputSample.Movement;
        _desiredVelicity = new Vector3(movement.x, 0, movement.y) * _movementSpeed * Time.fixedDeltaTime;
        _desiredVelicity = transform.TransformDirection(_desiredVelicity);
        _rg.MovePosition(transform.position + _desiredVelicity);
    }

    private float Clamp(float val, float min, float max)
    {
        if(val < min)
        {
            val = min;
        }
        else
        {
            if(val > max)
            {
                val = max;
            }
        }
        return val;
    }
}
