using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayEnd : MonoBehaviour
{
    public GameObject EndText;
    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    private void Update()
    {
        
    }

    private void TimeCheck()
    {
        if (TimeManager.Hour == 10 && TimeManager.Minute == 30)
            EndText.SetActive(true);
    }


}

