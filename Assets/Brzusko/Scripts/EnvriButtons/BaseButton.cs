using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseButton : MonoBehaviour, IInteractable
{

    protected  OneShootAnim _anim;
    protected virtual void Start() => _anim = GetComponent<OneShootAnim>();

    public virtual async void Interact()
    {
        _anim.PlayAnim();
    }
}
