using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SexSwitch3D : Switch3D
{
    [SerializeField]
    private GameObject[] _models;

    public override int Value 
    { 
        get => base.Value;
        set
        {
            base.Value = value;
            foreach(var model in _models)
                model.SetActive(false);
            _models[_index].SetActive(true);
        }
    }

    protected override void Start()
    {
        base.Start();

        foreach(var model in _models)
            model.SetActive(false);
        
        _models[_index].SetActive(true);
    }

    protected override void SetActiveOption(int lastIndex)
    {
        base.SetActiveOption(lastIndex);
        
        _models[lastIndex].SetActive(false);
        _models[_index].SetActive(true);
    }
}
