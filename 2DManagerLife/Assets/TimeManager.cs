using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static Action OnDayChanged;

    public bool isStoppedTime = true;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }
    public static int Day { get; private set; }
    public static string DayName { get; private set; }

    private float minuteToRealTime = 0.5f;
    private float timer;

    void Start()
    {
        Minute = 0;
        Hour = 12;
        Day = 0;
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
                if (Hour >= 24)
                {
                    Hour = 0;
                    Day += 1;
                    SwitchDayWeek(Day);
                    OnHourChanged?.Invoke();
                    OnDayChanged?.Invoke();
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

    public void HoursSkip(int hours)
    {
        Hour += hours;
    }

    public void MinutesSkip(int minutes)
    {
        Minute += minutes;
    }
    private void SwitchDayWeek(int Day)
    {
        switch (Day)
        {
            case 0:
                DayName = "Воскресенье";
                break;
            case 1:
                DayName = "Понедельник";
                break;
            case 2:
                DayName = "Вторник";
                break;
            case 3:
                DayName = "Среда";
                break;
            case 4:
                DayName = "Четверг";
                break;
            case 5:
                DayName = "Пятница";
                break;
            case 6:
                DayName = "Суббота";
                break;
        }
    }
}
