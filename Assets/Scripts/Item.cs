using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData Data;

    private bool _playerInTrigger = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerInTrigger)
        {
            if (Inventory.Instance.AddItem(Data))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerInTrigger = false;
    }
}