using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public ItemData[] Items = new ItemData[5];
    public GameObject cellContainer;


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

    void Start()
    {
        //заполнить из сохраненных данных
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            //item.Add(new Item());
        }
    }

    public void DisplayItems()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Transform cell = cellContainer.transform.GetChild(i);

            if (cell is null)
                continue;

            Transform icon = cell.GetChild(0);
            Image img = icon.GetComponent<Image>();

            if (Items[i] != null && Items[i].ID != 0)
            {
                img.enabled = true;
                img.sprite = Items[i]?.Icon;
            }
            else
            {
                img.enabled = false;
            }
        }
    }

    public bool HasNeedItems(List<ItemData> NeedItems)
    {
        List<ItemData> needItems = new List<ItemData>(NeedItems);
        List <ItemData> InventoryItems = new List<ItemData>();
        InventoryItems.AddRange(Items);

        for (int i = needItems.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < InventoryItems.Count; j++)
            {
                if (needItems[i] == InventoryItems[j])
                {
                    needItems.RemoveAt(i);

                    if (needItems.Count == 0)
                        return true;

                    InventoryItems.RemoveAt(j);
                    j = 0;
                }
            }
        }

        return false;
    }

    public void RemoveItems(List<ItemData> NeedItems)
    {
        for (int i = 0; i < NeedItems.Count; i++)
        {
            for (int j = 0; j < Items.Length; j++)
            {
                if (Items[j] != null && NeedItems[i].ID == Items[j].ID)
                {
                    Items[j] = null;
                    break;
                }
            }
        }

        DisplayItems();
    }

    public bool AddItem(ItemData Item)
    {
        for (int j = 0; j < Items.Length; j++)
        {
            if (Items[j] is null)
            {
                Items[j] = Item;
                DisplayItems();

                return true;
            }
        }
        DisplayItems();

        return false;
    }
}
