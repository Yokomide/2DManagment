using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public TaskData Data;


    private CanDoTask CanStartTask()
    {
        if (!(((Data.MinHours >= 0 && TimeManager.Hour >= Data.MinHours) &&
             (Data.MaxHours >= 0 && TimeManager.Hour <= Data.MaxHours) &&
             (Data.MinMinutes >= 0 && TimeManager.Minute >= Data.MinMinutes) &&
             (Data.MaxMinutes >= 0 && TimeManager.Minute <= Data.MaxMinutes)) ||
             (Data.MinHours < 0 || Data.MaxHours < 0 || Data.MinMinutes < 0 || Data.MaxMinutes < 0)))
            return CanDoTask.NoTime;

        if (PlayerStats.Instance.Energy < Data.NeedEnergy) 
            return CanDoTask.NoEnegry;

        if (!Inventory.Instance.HasNeedItems(Data.NeedItems))
            return CanDoTask.NoItem;


        /*bool condition = false;
        switch (Data.Task)
        {
            case Tasks.WaterFlower:
                condition = true;
                break;

            case Tasks.WashStove:
                condition = true;
                break;

            case Tasks.Sleep:
                condition = true;
                break;
        }*/

        return CanDoTask.CanDo;
    }

    public void TryStartTask()
    {
        switch (CanStartTask())
        {
            case CanDoTask.CanDo:
                StartTask();
                TrySetDialogNode(1);
                break;
            
            case CanDoTask.NoTime:
                TrySetDialogNode(2);
                break;
            
            case CanDoTask.NoItem:
                TrySetDialogNode(3);
                break;
            
            case CanDoTask.NoEnegry:
                TrySetDialogNode(4);
                break;
            
            default:
                break;
        }
    }

    private void TrySetDialogNode(int index)
    {
        if (VIDE_Data.VD.isActive)
        {
            VIDE_Data.VD.SetNode(index);
        }
    }

    public void StartTask()
    {
        TasksActions.GetAction(Data.Task).Invoke();
        OnEndTask();
    }

    public void OnEndTask()
    {
        //need
        Inventory.Instance.RemoveItems(Data.NeedItems);
        PlayerStats.Instance.RemoveEnergy(Data.NeedEnergy);
        TimeManager.Instance.MinutesSkip(Data.MinutesToComplete);
        TimeManager.Instance.HoursSkip(Data.HoursToComplete);

        //get
        foreach (var item in Data.GetItems)
        {
            Inventory.Instance.AddItem(item);
        }
        PlayerStats.Instance.AddEnergy(Data.GetEnergy);
    }

    public enum CanDoTask
    {
        CanDo,
        NoTime,
        NoItem,
        NoEnegry,
    };
}

public enum Tasks
{
    WaterFlower,
    WashStove,
    Sleep
};
