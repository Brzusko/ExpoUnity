using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch3D : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _options;

    public virtual int Value
    {
        get => _index;
        set
        {
            if(value > _maxIndex || value < 0)
            {
                _index = 0;
                return;
            }

            _index = value;

            _options[_index].SetActive(true);
        }
    }

    protected int _index;
    protected int _maxIndex;

    protected virtual void Start()
    {
        _index = 0;
        _maxIndex = _options.Length - 1;

        foreach(var option in _options)
            option.SetActive(false);
        
        _options[_index].SetActive(true);
    }

    protected virtual void SetActiveOption(int lastIndex)
    {
        _options[lastIndex].SetActive(false);
        _options[_index].SetActive(true);
    }

    public virtual void Increment()
    {
        var lastIndex = _index;
        _index++;

        if(_index > _maxIndex)
            _index = 0;
        
        SetActiveOption(lastIndex);
    }

    public virtual void Decrement()
    {
        var lastIndex = _index;
        _index--;

        if(_index < 0)
            _index = _maxIndex;
        
        SetActiveOption(lastIndex);
    }
}
