using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI phoneText;
    public TextMeshProUGUI dayText;

    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
        TimeManager.OnDayChanged += UpdateText;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
        TimeManager.OnDayChanged -= UpdateText;
    }

    private void UpdateTime()
    {
        timeText.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00}";
        phoneText.text = timeText.text;
    }
    private void UpdateText()
    {
        dayText.text = $"����: {TimeManager.Day}\n{TimeManager.DayName}";
    }
}
