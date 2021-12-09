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
        if (TimeManager.Hour == 00 && TimeManager.Minute == 10)
        {

            EndText.SetActive(true);
        }
        
    }


}

