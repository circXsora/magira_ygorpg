using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    public Image ItemImage;
    [SerializeField]
    private TMPro.TMP_Text ItemCountText;


    private InventoryItemSO item_obj;

    public Action OnClick;

    public void HandleClick() {
        OnClick?.Invoke();
    }

    public InventoryItemSO ItemObj { get => item_obj; set {
            item_obj = value;
            if (item_obj != null)
            {
                ItemImage.sprite = item_obj.item_image;
                ItemCountText.text = item_obj.item_count.ToString("00");

                ItemImage.enabled = true;
                ItemCountText.enabled = true;
            }
            else
            {
                ItemImage.enabled = false;
                ItemCountText.enabled = false;
            }
        } 
    }

}
