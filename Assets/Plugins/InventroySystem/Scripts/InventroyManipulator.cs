using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventroyManipulator : MonoBehaviour, IDragHandler
{
    public RectTransform UI;
    public void OnDrag(PointerEventData eventData)
    {
        UI.anchoredPosition += eventData.delta;
    }
}
