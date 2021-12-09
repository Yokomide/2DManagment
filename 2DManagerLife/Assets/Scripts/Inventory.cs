using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    List<Item> item;
    public GameObject cellContainer;
    public KeyCode takeButton;

    void Start()
    {
        item = new List<Item>();
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            item.Add(new Item());
        }
    }
    void Update()
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (Input.GetKeyDown(KeyCode.E)) {

                for (int i = 0; i < item.Count; i++)
                {
                    if (item[i].id == 0)
                    {
                        DisplayItems();
                        Destroy(GetComponent<Item>());
                        break;
                    }
                }
            }
        }

    }

    void DisplayItems()
    {
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id != 0)
            {
                Transform cell = cellContainer.transform.GetChild(i);
                Transform icon = cell.GetChild(0);
                Image img = icon.GetComponent<Image>();
                img.enabled = true;
                img.sprite = Resources.Load<Sprite>(item[i].pathIcon);

            }
        }
    }
}
