using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class TasksActions : MonoBehaviour
{
    #region // Start Actions

    public static Action<Action, Action> GetStartAction(Tasks task)
    {
        switch (task)
        {
            case Tasks.WaterFlower:
                return WaterFlower;

            case Tasks.WashStove:
                return WashStove;

            case Tasks.Programming:
                return GoToProgramming;
                break;

            default:
                return null;
                break;
        }

    }

    private static void GoToProgramming(Action winAction = null, Action loseAction = null)
    {
        ScenesManager.Instance.WinAction = winAction;
        ScenesManager.Instance.LoseAction = loseAction;
        ScenesManager.Instance.TryLoadScene(1);
    }

    private static void WashStove(Action winAction = null, Action loseAction = null)
    {
        ScenesManager.Instance.WinAction = winAction;
        ScenesManager.Instance.LoseAction = loseAction;
        ScenesManager.Instance.TryLoadScene(2);
    }

    private static void WaterFlower(Action winAction = null, Action loseAction = null)
    {
        winAction?.Invoke();
    }

    #endregion


    #region // OnComplete Actions

    public static Action GetOnCompleteAction(Tasks task)
    {
        switch (task)
        {
            case Tasks.WaterFlower:
                return CompleteFlowerTask;

            case Tasks.WashStove:
                return CompleteStoveTask;

            case Tasks.Sleep:
                return CompleteProgramming;
                break;

            default:
                return null;
                break;
        }

    }

    private static void CompleteProgramming()
    {
        
    }

    private static void CompleteStoveTask()
    {
        Counters.Instance.DirtyStove = 0;
    }

    private static void CompleteFlowerTask()
    {
        Counters.Instance.FlowerDryness = 0;
    }

    #endregion


    #region // OnFail Actions

    public static Action GetOnFailAction(Tasks task)
    {
        switch (task)
        {
            case Tasks.WashStove:
                return FailStoveTask;

            case Tasks.Programming:
                return FailProgrammingTask;

            default:
                return null;
        }

    }

    private static void FailStoveTask() { }
    private static void FailProgrammingTask() { }

    #endregion


    #region // CheckState Actions

    public static int CheckStateAction(Tasks task, int notDirtyStateIndex, int dirtyStateIndex, int monsterStateIndex, TaskData data = null)
    {
        switch (task)
        {
            case Tasks.WaterFlower:
                return CheckStateFlower(notDirtyStateIndex, dirtyStateIndex, monsterStateIndex);

            case Tasks.WashStove:
                return CheckStateStove(notDirtyStateIndex, dirtyStateIndex, monsterStateIndex);

            case Tasks.Programming:
                return CheckStateProgramming(notDirtyStateIndex, dirtyStateIndex, monsterStateIndex, data);
                break;

            default:
                return 0;
                break;
        }

    }

    private static int CheckStateProgramming(int notDirtyStateIndex, int dirtyStateIndex, int monsterStateIndex, TaskData data)
    {
        if (!(((data.MinHours >= 0 && TimeManager.Hour >= data.MinHours) &&
             (data.MaxHours >= 0 && TimeManager.Hour <= data.MaxHours) &&
             (data.MinMinutes >= 0 && TimeManager.Minute >= data.MinMinutes) &&
             (data.MaxMinutes >= 0 && TimeManager.Minute <= data.MaxMinutes)) ||
             (data.MinHours < 0 || data.MaxHours < 0 || data.MinMinutes < 0 || data.MaxMinutes < 0)))
            return dirtyStateIndex;
        else return notDirtyStateIndex;
    }

    private static int CheckStateFlower(int notDirtyStateIndex, int dirtyStateIndex, int monsterStateIndex)
    {
        int actualStateIndex = Counters.Instance.FlowerDryness > 20 ? dirtyStateIndex : notDirtyStateIndex;
        return actualStateIndex;
    }

    private static int CheckStateStove(int notDirtyStateIndex, int dirtyStateIndex, int monsterStateIndex)
    {
        int actualStateIndex = Counters.Instance.DirtyStove > 40 ? dirtyStateIndex : notDirtyStateIndex;
        return actualStateIndex;

        /*if (Counters.Instance.DirtyStove > 50)
        {
            if (false)//first wash
            {
                VIDE_Data.VD.SetNode(5);
            }
            else
            {
                VIDE_Data.VD.SetNode(9);
            }
        }
        else
        {
            VIDE_Data.VD.SetNode(8);
        }*/
    }

    #endregion
}
