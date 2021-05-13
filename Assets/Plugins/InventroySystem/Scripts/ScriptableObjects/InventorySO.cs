using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventroy", menuName = "Inventroy/New Inventroy")]
public class InventorySO : ScriptableObject
{
    public List<InventoryItemSO> items = new List<InventoryItemSO>();
}
