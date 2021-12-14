using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Task : MonoBehaviour
{
    public TaskData Data;

    // ѕереход на некоторые ноды в диалогах происходит программно. 
    // ѕоэтому после каждого редактировани€ диалога нужно проверить верность проставленных индексов
    [Space]
    [Header("DialogIndexes")]
    [Min(-1)] public int StateNotDirtyIndex = 0;
    [Min(-1)] public int StateDirtyIndex = 1;
    [Min(-1)] public int StateMonsterIndex = 2;

    [Space]
    [Min(-1)] public int CanDoIndex = 3;
    [Min(-1)] public int NoTimeIndex = 4;
    [Min(-1)] public int NoItemIndex = 5;
    [Min(-1)] public int NoEnergyIndex = 6;

    [Space]
    [Min(-1)] public int OnCompleteIndex = 7;
    [Min(-1)] public int OnFailIndex = 8;


    private VIDE_Assign _dialog;



    private void Awake()
    {
        _dialog = GetComponent<VIDE_Assign>();
    }


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

        if (Data.NeedItems.Count > 0 && !Inventory.Instance.HasNeedItems(Data.NeedItems))
            return CanDoTask.NoItem;

        return CanDoTask.CanDo;
    }


    public void CheckTaskState()
    {
        int actualStateIndex = TasksActions.CheckStateAction(Data.Task, StateNotDirtyIndex, StateDirtyIndex, StateMonsterIndex, Data);
        TrySetDialogNode(actualStateIndex);
    }


    public void TryStartTask()
    {
        switch (CanStartTask())
        {
            case CanDoTask.CanDo:
                TrySetDialogNode(CanDoIndex);
                break;
            
            case CanDoTask.NoTime:
                TrySetDialogNode(NoTimeIndex);
                break;
            
            case CanDoTask.NoItem:
                TrySetDialogNode(NoItemIndex);
                break;
            
            case CanDoTask.NoEnegry:
                TrySetDialogNode(NoEnergyIndex);
                break;
            
            default:
                break;
        }
    }


    private void TrySetDialogNode(int index)
    {
        if (_dialog is null)
        {
            _dialog = GetComponent<VIDE_Assign>();

            if (_dialog is null)
                return;
        }

        if (!VD.isActive)
        {
            VD.EndDialogue();
            Debug.Log("##1--------------");
            VIDEUIManagerCustom.Instance.Interact(_dialog);
            Debug.Log("##4--------------");
        }

        if (VD.isActive && index >= 0)
        {
            VD.SetNode(index);
        }
    }


    public void StartTask()
    {
        TasksActions.GetStartAction(Data.Task).Invoke(OnCompleteTask, OnFailTask);
    }


    public void OnCompleteTask()
    {
        ScenesManager.Instance.ClearActions();
        TasksActions.GetOnCompleteAction(Data.Task).Invoke();
        ResultOnComplete();
        TrySetDialogNode(OnCompleteIndex);
    }


    public void OnFailTask()
    {
        ScenesManager.Instance.ClearActions();
        TasksActions.GetOnFailAction(Data.Task).Invoke();
        ResultOnFail();
        TrySetDialogNode(OnFailIndex);
    }


    public void ResultOnComplete()
    {
        //need
        Inventory.Instance.RemoveItems(Data.NeedItems);
        ResultOnFail();

        //get
        foreach (var item in Data.GetItems)
        {
            Inventory.Instance.AddItem(item);
        }
        PlayerStats.Instance.AddEnergy(Data.GetEnergy);
    }


    public void ResultOnFail()
    {
        //need
        PlayerStats.Instance.RemoveEnergy(Data.NeedEnergy);
        TimeManager.Instance.MinutesSkip(Data.MinutesToComplete);
        TimeManager.Instance.HoursSkip(Data.HoursToComplete);
    }


    public void Log()
    {
        Debug.Log("haha");
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
    Programming,
    Sleep
};
