using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChanger : MonoBehaviour
{

    public GameTime DayTime;
     void OnTriggerEnter2D(Collider2D collision)
    {
        DayTime.SetDayTime();
    }


}
