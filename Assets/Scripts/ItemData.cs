using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Create ItemData")]
public class ItemData : ScriptableObject
{
    public string Name;
    public int ID;
    public Sprite Icon;
    public string PathPrefab;
}
