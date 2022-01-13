using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationApplyer : MonoBehaviour
{
    [SerializeField]
    private InputSampler _inputSampler;

    [SerializeField]
    private Animator _animator;

    private string _xAnimParam = "X";
    private int _xAnimHash = 0;
    private string _yAnimParam = "Y";
    private int _yAnimHash = 0;

    private float _x = 0;
    private float _y = 0;

    [SerializeField]
    private float _blendSpeed = 1.0f;

    private void Start() => GenerateHash();
    private void Update() => ApplyParams();

    private void GenerateHash()
    {
        _xAnimHash = Animator.StringToHash(_xAnimParam);
        _yAnimHash = Animator.StringToHash(_yAnimParam);
    }

    private void ApplyParams()
    {
        var inputSample = _inputSampler.CurrentImputSample;
        if(inputSample == null) return;

        BlendValues(inputSample);

        _animator.SetFloat(_xAnimHash, _x);
        _animator.SetFloat(_yAnimHash, _y);
    }

    private void BlendValues(InputSample sample)
    {
        _x = Mathf.MoveTowards(_x, sample.Movement.x, _blendSpeed * Time.deltaTime);
        _y = Mathf.MoveTowards(_y, sample.Movement.y, _blendSpeed * Time.deltaTime);
    }
}
