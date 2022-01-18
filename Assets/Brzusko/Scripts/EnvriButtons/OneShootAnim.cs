using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShootAnim : MonoBehaviour
{
    [SerializeField]
    private string _stateToPlay;

    private Animator _animator;

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public virtual void PlayAnim() => _animator.Play(_stateToPlay);
}
