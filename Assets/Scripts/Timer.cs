using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _delay;
    private Coroutine _timer;

    public event Func<Enemy> TimeChanged;

    private void OnEnable()
    {
        _delay = 2f;
        _timer = StartCoroutine(SetTimer(_delay));
    }

    private void OnDisable()
    {
        StopCoroutine(_timer);
    }

    private IEnumerator SetTimer(float seconds)
    { 
        WaitForSeconds wait = new WaitForSeconds(seconds);

        while (enabled)
        {
            yield return new WaitForSeconds(seconds);
            TimeChanged?.Invoke();
        }
    }
}
