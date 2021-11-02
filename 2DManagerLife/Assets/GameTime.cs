using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    private const float REAL_SECONDS_INGAME_DAY = 1500f;

    public Text timeText;
    public float StartTime;

    private float day;

    string hoursString;
    string minutesString;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        day += (Time.deltaTime / REAL_SECONDS_INGAME_DAY);
        float hoursPerDay = 24f;
        float minutesPerHour = 60f;

        float dayNormalized = day % 1f;

        hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");
        minutesString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

        timeText.text = hoursString + ":" + minutesString;

        if (Input.GetKey(KeyCode.T))
        {
            TimeReset();
        }
        if (Input.GetKey(KeyCode.R))
        {
            SetDayTime();
        }
    }

    public void TimeReset()
    {
        day = 0;
    }
    public void SetDayTime()
    {
        day += 0.042f;
    }

    // 00:00 - 0
    // 01:00 - 0.042f
    // 02:00 - 0.0835f
    // 03:00 -
    // 04:00 -
    // 05:00 -
    // 06:00 -
    // 07:00 -
    // 08:00 -
    // 09:00 -
    // 10:00 -
    // 11:00 -
    // 12:00 - 0.5f;
    // 13:00 -
    // 14:00 -
    // 15:00 -
    // 16:00 -
    // 17:00 -
    // 18:00 -
    // 19:00 -
    // 20:00 -
    // 21:00 -
    // 22:00 -
    // 23:00 -
}
