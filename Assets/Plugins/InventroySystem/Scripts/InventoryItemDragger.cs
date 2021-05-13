using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static bool dragging = false;
    private InventorySlot slot;
    private InventoryItemSO dragging_itemobj;
    private GameObject dragging_image;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot.ItemObj == null)
        {
            return;
        }
        dragging = true;
        dragging_itemobj = slot.ItemObj;
        dragging_image = Instantiate(slot.ItemImage.gameObject, slot.transform.parent.parent);
        slot.ItemObj = null;
        dragging_image.transform.position = eventData.position;
        dragging_image.AddComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragging_image == null)
        {
            return;
        }
        dragging_image.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragging_image == null)
        {
            return;
        }
        var targetObj = eventData.pointerCurrentRaycast.gameObject;
        if (targetObj != null)
        {
            var targetSlot = targetObj.GetComponent<InventorySlot>();
            if (targetSlot == null)
            {
                targetSlot = targetObj.GetComponentInParent<InventorySlot>();
            }

            if (targetSlot != null)
            {
                if (targetSlot.ItemObj == null)
                {
                    dragging_itemobj.slot_index = targetSlot.transform.GetSiblingIndex();
                    targetSlot.ItemObj = dragging_itemobj;
                }
                else
                {
                    var oldIndex = dragging_itemobj.slot_index;
                    var targetSlotItemObj = targetSlot.ItemObj;

                    dragging_itemobj.slot_index = targetSlot.transform.GetSiblingIndex();
                    targetSlot.ItemObj = dragging_itemobj;

                    targetSlotItemObj.slot_index = oldIndex;
                    slot.ItemObj = targetSlotItemObj;
                }
            }
        }
        Destroy(dragging_image.gameObject);
        dragging = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        slot = GetComponent<InventorySlot>();
    }

}
