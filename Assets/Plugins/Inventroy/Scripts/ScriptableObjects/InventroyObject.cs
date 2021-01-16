using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventroy", menuName = "Inventroy/New Inventroy")]
public class InventroyObject : ScriptableObject
{
    public List<ItemObject> items = new List<ItemObject>();
}
