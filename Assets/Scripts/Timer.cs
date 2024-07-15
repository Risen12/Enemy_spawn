using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Func<Enemy> TimeChanged;

    private float _delay;
    private float _timer;

    private void Start()
    {
        _delay = 2f;
        _timer = _delay;
    }

    private void Update()
    {
        if (Time.time >= _timer)
        {
            _timer = Time.time + _delay;
            TimeChanged?.Invoke();
        }
    }
}
