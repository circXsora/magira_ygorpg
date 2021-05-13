using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventroy/New Item")]
public class InventoryItemSO : ScriptableObject
{
    [SerializeField]
    public string item_name;
    [SerializeField]
    public Sprite item_image;


    [SerializeField]
    public int item_count = 1;
    [TextArea]
    [SerializeField]
    public string item_info;
    [SerializeField]
    public int slot_index = -1;
}
