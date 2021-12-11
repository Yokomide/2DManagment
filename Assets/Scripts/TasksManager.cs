using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksManager : MonoBehaviour
{
    /*public static TasksManager Instance;

    public List<Task> Tasks = new List<Task>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool HasNeedItems(Task task)
    {
        List<ItemData> InventoryItems = new List<ItemData>();
        InventoryItems.AddRange(Items);

        for (int i = NeedItems.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < InventoryItems.Count; j++)
            {
                if (NeedItems[i] == InventoryItems[j])
                {
                    NeedItems.RemoveAt(i);

                    if (NeedItems.Count == 0)
                        return true;

                    InventoryItems.RemoveAt(j);
                    j = 0;
                }
            }
        }

        return false;
    }*/
}
