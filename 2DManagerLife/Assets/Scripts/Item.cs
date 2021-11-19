using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private List<Item> itemList;
    public enum ItemType
    {
        sponge,
        detergent,
        water,
    }
    public ItemType itemType;
    public int amount;
}
