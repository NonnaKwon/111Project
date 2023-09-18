using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTime : MonoBehaviour
{
    Slider _slider;
    float _time;
    void Awake()
    {
        _slider = GetComponent<Slider>();
        SetMaxValue();
        _time = 0.0f;
    }

    void Update()
    {
        if (_slider.value > 0)
        {
            _time += Time.deltaTime;
            _slider.value = 1 - _time / Define.COOL_TIME;
        }
        else if (IsZero())
            _time = 0.0f;
        
    }

    public bool IsZero()
    {
        return _slider.value == 0;
    }

    public void SetMaxValue()
    {
        _slider.value = _slider.maxValue;
    }
}
