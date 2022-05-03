//------------------------------------------------------------------------------
//  <copyright file="EntitySelection.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/11/26 23:38:24
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO.Entity.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BBYGO
{
    public enum SelectionState
    {
        NotHover,
        Hover,
    }

    public abstract class EntitySelector : EntityGear, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public virtual bool CanSelect { get; set; } = true;
        private SelectionState state = SelectionState.NotHover;
        public SelectionState State
        {
            get => state;
            private set
            {
                if (state != value)
                {
                    state = value;
                    OnSelectStateChanged?.Invoke(GetOwner(), value);
                }
            }
        }

        public event EventHandler<SelectionState> OnSelectStateChanged;
        public event EventHandler<PointerEventData> OnPointerClicked;
        public event EventHandler<PointerEventData> OnBeginDragEvent;
        public event EventHandler<PointerEventData> OnDragEvent;
        public event EventHandler<PointerEventData> OnEndDragEvent;


        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClicked?.Invoke(GetOwner(), eventData);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            State = SelectionState.Hover;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            State = SelectionState.NotHover;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            OnDragEvent?.Invoke(this, eventData);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            OnDragEvent?.Invoke(this, eventData);
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            OnBeginDragEvent?.Invoke(this, eventData);
        }
    }
}