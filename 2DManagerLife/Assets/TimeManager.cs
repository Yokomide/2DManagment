using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public bool isStoppedTime = true;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    private float minuteToRealTime = 0.5f;
    private float timer;

    void Start()
    {
        Minute = 0;
        Hour = 12;
        timer = minuteToRealTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStoppedTime == false)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Minute++;
                OnMinuteChanged?.Invoke();
                if (Minute >= 60)
                {
                    Hour++;
                    Minute = 0;
                    OnHourChanged?.Invoke();
                }
                if (Hour == 24)
                {
                    Hour = 0;
                    OnHourChanged?.Invoke();
                }
                timer = minuteToRealTime;
            }
        }
       
    }
    public void StartTime()
    {
        isStoppedTime = false;
    }
    public void StopTime()
    {
        isStoppedTime = true;
    }

    public void TimeSkip(int Actions)
    {
        switch (Actions)
        {
            case 1: Hour+= 2;
                break;
            case 2: Hour+= 5;
                break;
        }
    }
}
