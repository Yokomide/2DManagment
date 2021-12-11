using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "TaskData", menuName = "Create TaskData")]
public class TaskData : ScriptableObject
{
    public Tasks Task;

    [Space]
    [Header("Need")]
    [Range(-1, 23)] public int MinHours = -1;
    [Range(-1, 59)] public int MinMinutes = -1;
    [Range(-1, 23)] public int MaxHours = -1;
    [Range(-1, 59)] public int MaxMinutes = -1;
    [Space]
    [Range(0, 100)] public int NeedEnergy = 0;
    [Range(0, 23)] public int HoursToComplete = 0;
    [Range(0, 59)] public int MinutesToComplete = 0;
    public List<ItemData> NeedItems = new List<ItemData>();
    
    [Space]
    [Header("Get")]
    [Range(0, 100)] public int GetEnergy = 0;
    public int CatFrienliness = 0;
    public int Independence = 0;
    public List<ItemData> GetItems = new List<ItemData>();
}
