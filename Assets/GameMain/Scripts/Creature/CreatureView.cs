//------------------------------------------------------------------------------
//  <copyright file="CreatureEneity.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:45:09
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BBYGO
{
    public abstract class CreatureView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public CreatureBindings Bindings { get; set; }

        public event EventHandler<PointerEventData> OnPointerClicked;
        public event EventHandler<PointerEventData> OnPointerExited;
        public event EventHandler<PointerEventData> OnPointerEntered;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClicked?.Invoke(this, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExited?.Invoke(this, eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEntered?.Invoke(this, eventData);
        }

        public virtual async Task Show()
        {
            gameObject.SetActive(true);
        }
        public virtual async Task Hide()
        {
            gameObject.SetActive(false);
        }
        public virtual async Task Destroy()
        {
            Destroy(gameObject);
        }


    }
}