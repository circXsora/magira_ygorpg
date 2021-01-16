using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventroyManager : MonoBehaviour
{
    public GameObject UI;

    public GridLayoutGroup Grid;

    private Slot[] slots;

    public InventroyObject inventroy_obj;

    public TMPro.TMP_Text IntrductionText;

    public bool IsOpen { get => UI.activeSelf; }

    private void Start()
    {
        slots = Grid.GetComponentsInChildren<Slot>();
        foreach (var slot in slots)
            slot.OnClick += () => { if (slot.ItemObj != null) IntrductionText.text = slot.ItemObj.item_info; };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UI.SetActive(!IsOpen);
        }
        RefreshInventroy();
    }

    private void RefreshInventroy()
    {
        if (ItemOnDrag.dragging)
        {
            return;
        }
        for (int i = 0; i < inventroy_obj.items.Count; i++)
        {
            if (inventroy_obj.items[i].slot_index != -1)
            {
                slots[inventroy_obj.items[i].slot_index].ItemObj = inventroy_obj.items[i];
            }
            else
            {
                slots[i].ItemObj = inventroy_obj.items[i];
                slots[i].ItemObj.slot_index = i;
            }
        }
    }
}
