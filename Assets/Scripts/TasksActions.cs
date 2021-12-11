using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TasksActions : MonoBehaviour
{
    public static Action GetAction(Tasks task)
    {
        switch (task)
        {
            case Tasks.WaterFlower:
                return WaterFlower;
                break;

            /*case Tasks.WashStove:
                break;

            case Tasks.Sleep:
                break;*/

            default:
                return null;
                break;
        }

    }

    public static void WaterFlower()
    {
        Counters.Instance.FlowerDryness = 0;
    }

    public void NeedWaterFlower()
    {
        VIDE_Data.VD.SetNode(Counters.Instance.FlowerDryness > 20 ? 10 : 11);
    }
}
