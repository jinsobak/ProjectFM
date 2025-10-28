using UnityEngine;
using System;

public class TimeData
{
    public float time;
    private float curTime = 0f;
    public bool timerActivated { get; private set; } = false;
    public event Action onTimerEnd;

    public TimeData(float time) 
    {
        this.time = time;
    }

    public void StartTimer()
    {
        timerActivated = true;
        curTime = time;
    }

    public void DiscountCooltime()
    {
        if (timerActivated)
        {
            if (curTime > 0)
            {
                curTime -= Time.deltaTime;
            }
            else
            {
                timerActivated = false;
                onTimerEnd?.Invoke();
            }
        }
    }

    public void RegisterCooltimeEndAction(Action onTimerEnd)
    {
        this.onTimerEnd += onTimerEnd;
    }

    public void UnRegisterCooltimeEndAction(Action onTimerEnd)
    {
        this.onTimerEnd -= onTimerEnd;
    }
}
