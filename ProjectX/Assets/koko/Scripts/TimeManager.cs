using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    float slowValue = 0;
    [SerializeField]
    float slowTime = 0;

    float timer = 0;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            Time.timeScale = slowValue;
        }
        else
        {
            timer = 0;

            Time.timeScale = 1;
        }
    }

    public void SetSlow(float _value, float _time)
    {
        slowValue = _value;
        slowTime = _time;
        timer = slowTime;
    }
}
